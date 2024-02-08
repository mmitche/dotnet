﻿# List of components

To enable full offline source-building of the VMR, we have no other choice than to synchronize all the necessary code into the VMR. This also includes any code referenced via git submodules. More details on why and how this is done can be found here:
- [Strategy for managing external source dependencies](src/arcade/Documentation/UnifiedBuild/VMR-Strategy-For-External-Source.md)
- [Source Synchronization Process](src/arcade/Documentation/UnifiedBuild/VMR-Design-And-Operation.md#source-synchronization-process)

## Detailed list

<!-- component list beginning -->
- `src/arcade`  
*[dotnet/arcade@8cfc948](https://github.com/dotnet/arcade/tree/8cfc9489d3e51071fedec9dcb99071dc912718bd)*
- `src/aspire`  
*[dotnet/aspire@87d5246](https://github.com/dotnet/aspire/tree/87d5246ddfc1fb9b07fcdf7b4b42830f67427ab9)*
- `src/aspnetcore`  
*[dotnet/aspnetcore@9ff5503](https://github.com/dotnet/aspnetcore/tree/9ff55037da0643107a1e4256f77b227f5716eccf)*
    - `src/aspnetcore/src/submodules/googletest`  
    *[google/googletest@4565741](https://github.com/google/googletest/tree/456574145cf71a5375777cab58453acfd92a920b)*
    - `src/aspnetcore/src/submodules/MessagePack-CSharp`  
    *[aspnet/MessagePack-CSharp@ecc4e18](https://github.com/aspnet/MessagePack-CSharp/tree/ecc4e18ad7a0c7db51cd7e3d2997a291ed01444d)*
- `src/cecil`  
*[dotnet/cecil@b8c2293](https://github.com/dotnet/cecil/tree/b8c2293cd1cbd9d0fe6f32d7b5befbd526b5a175)*
- `src/command-line-api`  
*[dotnet/command-line-api@46fea71](https://github.com/dotnet/command-line-api/tree/46fea71e3d98dad0d676950522004b7f295dd372)*
- `src/deployment-tools`  
*[dotnet/deployment-tools@9abdab1](https://github.com/dotnet/deployment-tools/tree/9abdab1d923b427c26685d793e9ddc8344f3da5c)*
- `src/diagnostics`  
*[dotnet/diagnostics@79b59c5](https://github.com/dotnet/diagnostics/tree/79b59c505405b9bee1d62dfa73dfb9750b2d4376)*
- `src/emsdk`  
*[dotnet/emsdk@c7b4dbc](https://github.com/dotnet/emsdk/tree/c7b4dbc857259968a0892cf94cfa9ae4f2ca53cd)*
- `src/format`  
*[dotnet/format@fcce27f](https://github.com/dotnet/format/tree/fcce27f7dd718a81bc5063adc50eba00c5997cf9)*
- `src/fsharp`  
*[dotnet/fsharp@daad9c4](https://github.com/dotnet/fsharp/tree/daad9c41c97234ced41aef2c7b6154d0a530124e)*
- `src/installer`  
*[dotnet/installer@6246267](https://github.com/dotnet/installer/tree/62462675dd7e24d9311d76b7a39db0070b6669ee)*
- `src/msbuild`  
*[dotnet/msbuild@668b199](https://github.com/dotnet/msbuild/tree/668b19903aec6334c05190cb336a10b9a9aba01f)*
- `src/nuget-client`  
*[nuget/nuget.client@d55931a](https://github.com/nuget/nuget.client/tree/d55931a69dcda3dcb87ba46a09fe268e0febc223)*
    - `src/nuget-client/submodules/NuGet.Build.Localization`  
    *[NuGet/NuGet.Build.Localization@f15db7b](https://github.com/NuGet/NuGet.Build.Localization/tree/f15db7b7c6f5affbea268632ef8333d2687c8031)*
- `src/razor`  
*[dotnet/razor@a8e386f](https://github.com/dotnet/razor/tree/a8e386f7e40ea661ac35e544edcd9f466c4a83ed)*
- `src/roslyn`  
*[dotnet/roslyn@c3d3595](https://github.com/dotnet/roslyn/tree/c3d3595593ece465ca4c2bdb01f15306da352b89)*
- `src/roslyn-analyzers`  
*[dotnet/roslyn-analyzers@d0311fb](https://github.com/dotnet/roslyn-analyzers/tree/d0311fbcd264aaba20544e611e04976c4ada57da)*
- `src/runtime`  
*[dotnet/runtime@9dd8f52](https://github.com/dotnet/runtime/tree/9dd8f5205784cabedd639fed1959bbfd95b2efd1)*
- `src/scenario-tests`  
*[dotnet/scenario-tests@bfde902](https://github.com/dotnet/scenario-tests/tree/bfde902a10d7b672f4fc7e844198ede405dbb9c6)*
- `src/sdk`  
*[dotnet/sdk@9c21e56](https://github.com/dotnet/sdk/tree/9c21e56deb825b6768307969451ea3f13c999fb5)*
- `src/source-build-externals`  
*[dotnet/source-build-externals@2c52f66](https://github.com/dotnet/source-build-externals/tree/2c52f66055a098987321c8fe96472679661c4071)*
    - `src/source-build-externals/src/abstractions-xunit`  
    *[xunit/abstractions.xunit@b75d54d](https://github.com/xunit/abstractions.xunit/tree/b75d54d73b141709f805c2001b16f3dd4d71539d)*
    - `src/source-build-externals/src/application-insights`  
    *[Microsoft/ApplicationInsights-dotnet@5e2e7dd](https://github.com/Microsoft/ApplicationInsights-dotnet/tree/5e2e7ddda961ec0e16a75b1ae0a37f6a13c777f5)*
    - `src/source-build-externals/src/azure-activedirectory-identitymodel-extensions-for-dotnet`  
    *[AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet@a607fa5](https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/tree/a607fa5e0005a6178cf1d2fed4fa0f8179cdb186)*
    - `src/source-build-externals/src/cssparser`  
    *[dotnet/cssparser@0d59611](https://github.com/dotnet/cssparser/tree/0d59611784841735a7778a67aa6e9d8d000c861f)*
    - `src/source-build-externals/src/docker-creds-provider-2.2.0`  
    *[mthalman/docker-creds-provider@5701f66](https://github.com/mthalman/docker-creds-provider/tree/5701f6667c1fbd805684857baaa860383bbdfed7)*
    - `src/source-build-externals/src/docker-creds-provider-2.2.1`  
    *[mthalman/docker-creds-provider@6c73fa4](https://github.com/mthalman/docker-creds-provider/tree/6c73fa4784795ae07f49305a057abf5c473d2adb)*
    - `src/source-build-externals/src/humanizer`  
    *[Humanizr/Humanizer@3ebc38d](https://github.com/Humanizr/Humanizer/tree/3ebc38de585fc641a04b0e78ed69468453b0f8a1)*
    - `src/source-build-externals/src/MSBuildLocator`  
    *[microsoft/MSBuildLocator@e0281df](https://github.com/microsoft/MSBuildLocator/tree/e0281df33274ac3c3e22acc9b07dcb4b31d57dc0)*
    - `src/source-build-externals/src/newtonsoft-json`  
    *[JamesNK/Newtonsoft.Json@0a2e291](https://github.com/JamesNK/Newtonsoft.Json/tree/0a2e291c0d9c0c7675d445703e51750363a549ef)*
    - `src/source-build-externals/src/spectre-console`  
    *[spectreconsole/spectre.console@7397169](https://github.com/spectreconsole/spectre.console/tree/7397169a2757dc3657598bdea4ac222c0f283425)*
    - `src/source-build-externals/src/xunit`  
    *[xunit/xunit@f110e5b](https://github.com/xunit/xunit/tree/f110e5bee5dfd4c08339587c9c3df9292fcb597c)*
    - `src/source-build-externals/src/xunit/src/xunit.assert/Asserts`  
    *[xunit/assert.xunit@5c8c10e](https://github.com/xunit/assert.xunit/tree/5c8c10e085eb42f39f2fe0b40c94bf56649eb0a4)*
    - `src/source-build-externals/src/xunit/tools/build`  
    *[xunit/build-tools@8e186b0](https://github.com/xunit/build-tools/tree/8e186b0f8e398796e75453f3f18952b06d29fdfd)*
    - `src/source-build-externals/src/xunit/tools/media`  
    *[xunit/media@5738b6e](https://github.com/xunit/media/tree/5738b6e86f08e0389c4392b939c20e3eca2d9822)*
- `src/source-build-reference-packages`  
*[dotnet/source-build-reference-packages@a739c05](https://github.com/dotnet/source-build-reference-packages/tree/a739c05eb1a5200d7fa2f1e3977b4dc54fdec36a)*
- `src/sourcelink`  
*[dotnet/sourcelink@7378b26](https://github.com/dotnet/sourcelink/tree/7378b26120437e4984e322d07bfd5028e10bc5ad)*
- `src/symreader`  
*[dotnet/symreader@2902dcf](https://github.com/dotnet/symreader/tree/2902dcf06494391dc65552fd0743b7d426c550fb)*
- `src/templating`  
*[dotnet/templating@d7dc9b3](https://github.com/dotnet/templating/tree/d7dc9b3a8d9735ecdc71369cab62ead41a14c6ed)*
- `src/test-templates`  
*[dotnet/test-templates@23a6d5c](https://github.com/dotnet/test-templates/tree/23a6d5c63f8a3c8b7314334906fd9681baf60969)*
- `src/vstest`  
*[microsoft/vstest@4e52655](https://github.com/microsoft/vstest/tree/4e52655f318fbc1677b4274b3f0add42609be0df)*
- `src/windowsdesktop`  
*[dotnet/windowsdesktop@4d48c99](https://github.com/dotnet/windowsdesktop/tree/4d48c996872e708f750329c7c784870858a40c96)*
- `src/winforms`  
*[dotnet/winforms@5207c65](https://github.com/dotnet/winforms/tree/5207c6554bb20236bd91c6083c3e1ee3c76c9402)*
- `src/wpf`  
*[dotnet/wpf@3db410e](https://github.com/dotnet/wpf/tree/3db410e6645fd9a13e9b1c13f36ab32087b5b970)*
- `src/xdt`  
*[dotnet/xdt@23e11f8](https://github.com/dotnet/xdt/tree/23e11f8312f853a3f694c6680c0e3762bdf1d9fd)*
- `src/xliff-tasks`  
*[dotnet/xliff-tasks@73f0850](https://github.com/dotnet/xliff-tasks/tree/73f0850939d96131c28cf6ea6ee5aacb4da0083a)*
<!-- component list end -->

The repository also contains a [JSON manifest](https://github.com/dotnet/dotnet/blob/main/src/source-manifest.json) listing all components in a machine-readable format.
