using Microsoft.DotNet.VersionTools.Automation;
using System.Text.RegularExpressions;
using System.Xml.Linq;

enum AssetType
{
    Blob,
    Package,
    Unknown
}

class AssetMapping
{
    public string Id { get; set; }

    public AssetType AssetType { get; set; } = AssetType.Unknown;
    public bool DiffElementFound { get => DiffManifestElement != null; }
    public bool DiffFileFound { get => DiffFilePath != null; }

    public string DiffFilePath { get; set; }
    public XElement DiffManifestElement { get; set; }

    public string BaseBuildFilePath { get; set; }
    public XElement BaseBuildManifestElement
    {
        get; set;
    }
}

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: Program <manifestPath> <assetBasePath> <outputFilePath>");
            return;
        }

        string manifestPath = args[0];
        string assetBasePath = args[1];
        string outputFilePath = args[2];

        // Load the XML file
        XDocument vmrMergedManifestContent = XDocument.Load(manifestPath);

        // Get all files in the assets folder, including subfolders
        var allFiles = Directory.GetFiles(assetBasePath, "*", SearchOption.AllDirectories);

        List<string> missingPackagesShipping = new List<string>();
        List<string> missingPackagesNonShipping = new List<string>();
        List<string> missingBlobsShipping = new List<string>();
        List<string> missingBlobsNonShipping = new List<string>();
        List<string> misclassifiedBlobsVmrShipping = new List<string>();
        List<string> misclassifiedBlobsVmrNonShipping = new List<string>();

        List<AssetMapping> assetMappings = new List<AssetMapping>();

        // Walk the top-level directories of the asset base path, and find the MergedManifest under each
        // one. The MergedManifest.xml contains the list of outputs produced by the repo.

        foreach (var baseDirectory in Directory.GetDirectories(assetBasePath, "*", SearchOption.TopDirectoryOnly))
        {
            // Find the merged manifest underneath this directory
            // (e.g. <assetBasePath>/arcade/nonshipping/<version>>/MergedManifest.xml)

            string repoMergedManifestPath = Directory.GetFiles(baseDirectory,
                "MergedManifest.xml", SearchOption.AllDirectories)
                .FirstOrDefault();

            if (repoMergedManifestPath == null)
            {
                Console.WriteLine($"Failed to find merged manifest for {baseDirectory}");
                continue;
            }

            var repoBuildMergeManifestContent = XDocument.Load(repoMergedManifestPath);
            assetMappings.AddRange(MapFilesForManifest(vmrMergedManifestContent, baseDirectory, repoMergedManifestPath, repoBuildMergeManifestContent));
        }

        // Now that we have the asset mappings, we can check for missing, misclassified, or incorrect assets
        EvaluatePackages(missingPackagesShipping, missingPackagesNonShipping, assetMappings);

        /*foreach (var file in allFiles)
        {
            bool foundMatchBlob = false;

            if (file.Contains("\\Packages\\", StringComparison.OrdinalIgnoreCase))
            {
                // Skip source build intermediates
                if (file.Contains("SourceBuild.Intermediate"))
                {
                    continue;
                }

                // Find the package in the VMR's merged manifest.
                var nupkgInfo = nupkgInfoFactory.CreateNupkgInfo(file);
                var matchingPackage = vmrMergedManifestContent.Descendants("Package")
                    .FirstOrDefault(p => p.Attribute("Id")?.Value == nupkgInfo.Id);

                if (matchingPackage == null)
                {
                    if (file.Contains("\\nonshipping\\"))
                    {
                        missingPackagesNonShipping.Add(file.Substring(assetBasePath.Length - 1));
                    }
                    else
                    {
                        missingPackagesShipping.Add(file.Substring(assetBasePath.Length - 1));
                    }
                }
            }

            if (!file.Contains("\\Packages\\", StringComparison.OrdinalIgnoreCase))
            {
                if (file.EndsWith("manifest.json") || file.EndsWith("release.json") || file.EndsWith("MergedManifest.xml") || file.Contains("wixpack"))
                {
                    continue;
                }

                foreach (var blob in vmrMergedManifestContent.Descendants("Blob"))
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                    string id = GetModifiedBlobPath(blob.Attribute("Id").Value);
                    string repoOrigin = blob.Attribute("RepoOrigin")?.Value;
                    string shippingExpected = blob.Attribute("DotNetReleaseShipping")?.Value == "true" ? "shipping" : "nonshipping";
                    string shippingWrong = blob.Attribute("DotNetReleaseShipping").Value == "true" ? "nonshipping" : "shipping";

                    string expectedLocation = $"{assetBasePath}/{repoOrigin}/{shippingExpected}/assets/{id}".Replace("\\", "/");
                    expectedLocation = Regex.Replace(expectedLocation, "/d\\+", "\\d+");
                    string unexpectedLocation = $"{assetBasePath}/{repoOrigin}/{shippingWrong}/assets/{id}".Replace("\\", "/");
                    unexpectedLocation = Regex.Replace(unexpectedLocation, "/d\\+", "\\d+");

                    string packageOnDisk = file.Replace("\\", "/");

                    if (Regex.IsMatch(packageOnDisk, expectedLocation))
                    {
                        foundMatchBlob = true;
                        break;
                    }
                    else if (Regex.IsMatch(packageOnDisk, unexpectedLocation))
                    {
                        if (blob.Attribute("DotNetReleaseShipping").Value == "true")
                        {
                            misclassifiedBlobsVmrShipping.Add(file.Substring(assetBasePath.Length - 1));
                        }
                        else
                        {
                            misclassifiedBlobsVmrNonShipping.Add(file.Substring(assetBasePath.Length - 1));
                        }
                        foundMatchBlob = true;
                        break;
                    }
                }

                if (!foundMatchBlob)
                {
                    if (file.Contains("\\nonshipping\\"))
                    {
                        missingBlobsNonShipping.Add(file.Substring(assetBasePath.Length - 1));
                    }
                    else
                    {
                        missingBlobsShipping.Add(file.Substring(assetBasePath.Length - 1));
                    }
                }
            }
        }*/

        Directory.CreateDirectory(outputFilePath);

        File.WriteAllLines(Path.Combine(outputFilePath, "MissingShippingPackages.txt"), missingPackagesShipping);
        File.WriteAllLines(Path.Combine(outputFilePath, "MissingNonShippingPackages.txt"), missingPackagesNonShipping);
        File.WriteAllLines(Path.Combine(outputFilePath, "MissingShippingBlobs.txt"), missingBlobsShipping);
        File.WriteAllLines(Path.Combine(outputFilePath, "MissingNonShippingBlobs.txt"), missingBlobsNonShipping);
        File.WriteAllLines(Path.Combine(outputFilePath, "NonShippingBlobsMarkedShippingByVMR.txt"), misclassifiedBlobsVmrShipping);
        File.WriteAllLines(Path.Combine(outputFilePath, "ShippingBlobsMarkedNonShippingByVMR.txt"), misclassifiedBlobsVmrNonShipping);
    }

    private static void EvaluatePackages(List<string> missingPackagesShipping, List<string> missingPackagesNonShipping, List<AssetMapping> assetMappings)
    {
        foreach (var mapping in assetMappings.Where(a => a.AssetType == AssetType.Package))
        {
            // Filter away mappings that we do not care about
            if (mapping.BaseBuildManifestElement.Attribute("Id").Value.Contains("Microsoft.SourceBuild.Intermediate"))
            {
                continue;
            }

            // Check if the package is missing in the VMR
            if (!mapping.DiffElementFound)
            {
                string detailsString = $"{mapping.Id} ({mapping.BaseBuildFilePath ?? "base file only in manifest"})";
                if (mapping.BaseBuildManifestElement.Attribute("NonShipping")?.Value == "true")
                {
                    missingPackagesNonShipping.Add(detailsString);
                }
                else
                {
                    missingPackagesShipping.Add(detailsString);
                }
            }

            // Now perform additional tests over the package content
        }
    }

    private static List<AssetMapping> MapFilesForManifest(XDocument vmrMergedManifestContent, string baseDirectory, string repoMergedManifestPath, XDocument repoBuildMergeManifestContent)
    {
        List<AssetMapping> assetMappings = new();
        Console.WriteLine($"Mapping base build outputs in {repoMergedManifestPath} to VMR.");

        // For each top level element, switch on the name of the element. Could be Blob or Package
        foreach (var element in repoBuildMergeManifestContent.Descendants())
        {
            switch (element.Name.LocalName)
            {
                case "Blob":
                    assetMappings.Add(MapBlob(vmrMergedManifestContent, element, baseDirectory, null));
                    break;
                case "Package":
                    assetMappings.Add(MapPackage(vmrMergedManifestContent, element, baseDirectory, null));
                    break;
                case "Build":
                case "SigningInformation":
                case "FileSignInfo":
                case "FileExtensionSignInfo":
                case "StrongNameSignInfo":
                case "CertificatesSignInfo":
                    // Nothing to do
                    break;
                default:
                    Console.WriteLine("Unknown type of top level element in repo merged manifest: " + element.Name);
                    break;
            }
        }

        return assetMappings;
    }

    private static AssetMapping MapBlob(XDocument diffMergedManifestContent, XElement baseElement, string basePath, string diffPath)
    {
        return new AssetMapping
        {
            DiffFilePath = null,
            DiffManifestElement = null,
            BaseBuildFilePath = null,
            BaseBuildManifestElement = baseElement,
            AssetType = AssetType.Blob
        };
    }

    private static AssetMapping MapPackage(XDocument diffMergedManifestContent, XElement baseElement, string basePath, string diffPath)
    {
        string packageId = baseElement.Attribute("Id")?.Value;
        string basePackageVersion = baseElement.Attribute("Version")?.Value;
        bool basePackageIsShipping = baseElement.Attribute("NonShipping")?.Value != "true";
        string basePackageShippingPathElement = basePackageIsShipping ? "shipping" : "nonshipping";
        string baseFilePath = Path.Combine(basePath, basePackageShippingPathElement, "packages", $"{packageId}.{basePackageVersion}.nupkg");

        if (!File.Exists(baseFilePath))
        {
            // Find the diff file path
            baseFilePath = null;
        }

        var diffPackageElement = diffMergedManifestContent.Descendants("Package")
            .FirstOrDefault(p => p.Attribute("Id")?.Value == packageId);

        string diffFilePath = null;
        if (diffPackageElement != null)
        {
            string diffPackageVersion = diffPackageElement.Attribute("Version")?.Value;
            bool diffPackageIsShipping = diffPackageElement.Attribute("NonShipping")?.Value != "true";
            string diffPackageShippingPathElement = diffPackageIsShipping ? "shipping" : "nonshipping";
            diffFilePath = Path.Combine(basePath, diffPackageShippingPathElement, "packages", $"{packageId}.{diffPackageVersion}.nupkg");
            if (!File.Exists(diffFilePath))
            {
                diffFilePath = null;
            }
        }

        return new AssetMapping
        {
            Id = packageId,
            DiffFilePath = diffFilePath,
            DiffManifestElement = diffPackageElement,
            BaseBuildFilePath = baseFilePath,
            BaseBuildManifestElement = baseElement,
            AssetType = AssetType.Package
        };
    }

    static string GetModifiedBlobPath(string inputString)
    {
        var parts = inputString.Split('/');
        var modifiedParts = parts.Select(ModifyNamePart).ToArray();
        var newString = string.Join('/', modifiedParts);

        if (newString.StartsWith("assets/"))
        {
            newString = newString.Substring(7);
        }

        return newString;
    }

    static string ModifyNamePart(string name)
    {
        string pattern = @"(?<=[-.])(\d{3,})(?=[-.])|(?<=[-.])(\d{3,})$";
        var matches = Regex.Matches(name, pattern);

        if (matches.Count > 0)
        {
            var lastMatch = matches[matches.Count - 1].Value;
            name = Regex.Replace(name, Regex.Escape(lastMatch), "\\d+");
        }

        if (matches.Count > 1)
        {
            var secondLastMatch = matches[matches.Count - 2].Value;
            name = Regex.Replace(name, Regex.Escape(secondLastMatch), "\\d+");
        }

        return name;
    }
}
