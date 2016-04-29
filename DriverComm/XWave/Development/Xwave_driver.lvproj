<?xml version='1.0' encoding='UTF-8'?>
<Project Type="Project" LVVersion="14008000">
	<Item Name="My Computer" Type="My Computer">
		<Property Name="NI.SortType" Type="Int">3</Property>
		<Property Name="server.app.propertiesEnabled" Type="Bool">true</Property>
		<Property Name="server.control.propertiesEnabled" Type="Bool">true</Property>
		<Property Name="server.tcp.enabled" Type="Bool">false</Property>
		<Property Name="server.tcp.port" Type="Int">0</Property>
		<Property Name="server.tcp.serviceName" Type="Str">My Computer/VI Server</Property>
		<Property Name="server.tcp.serviceName.default" Type="Str">My Computer/VI Server</Property>
		<Property Name="server.vi.callsEnabled" Type="Bool">true</Property>
		<Property Name="server.vi.propertiesEnabled" Type="Bool">true</Property>
		<Property Name="specify.custom.address" Type="Bool">false</Property>
		<Item Name="_init_vartree" Type="Folder" URL="../_init_vartree">
			<Property Name="NI.DISK" Type="Bool">true</Property>
		</Item>
		<Item Name="DataloggingIcon.ico" Type="Document" URL="../DataloggingIcon.ico"/>
		<Item Name="sysvarcfg.xml" Type="Document" URL="../sysvarcfg.xml"/>
		<Item Name="Conns_Params.ctl" Type="VI" URL="../type_def/Conns_Params.ctl"/>
		<Item Name="DataCluster.ctl" Type="VI" URL="../type_def/DataCluster.ctl"/>
		<Item Name="Init.vi" Type="VI" URL="../Init.vi"/>
		<Item Name="Vars_Init.vi" Type="VI" URL="../Vars_Init.vi"/>
		<Item Name="ConsCluster.ctl" Type="VI" URL="../ConsCluster.ctl"/>
		<Item Name="Unflatten.vi" Type="VI" URL="../Unflatten.vi"/>
		<Item Name="Dependencies" Type="Dependencies">
			<Item Name="vi.lib" Type="Folder">
				<Item Name="compatCalcOffset.vi" Type="VI" URL="/&lt;vilib&gt;/_oldvers/_oldvers.llb/compatCalcOffset.vi"/>
				<Item Name="compatFileDialog.vi" Type="VI" URL="/&lt;vilib&gt;/_oldvers/_oldvers.llb/compatFileDialog.vi"/>
				<Item Name="compatOpenFileOperation.vi" Type="VI" URL="/&lt;vilib&gt;/_oldvers/_oldvers.llb/compatOpenFileOperation.vi"/>
				<Item Name="FindCloseTagByName.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/xml.llb/FindCloseTagByName.vi"/>
				<Item Name="FindElement.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/xml.llb/FindElement.vi"/>
				<Item Name="FindElementStartByName.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/xml.llb/FindElementStartByName.vi"/>
				<Item Name="FindEmptyElement.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/xml.llb/FindEmptyElement.vi"/>
				<Item Name="FindFirstTag.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/xml.llb/FindFirstTag.vi"/>
				<Item Name="FindMatchingCloseTag.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/xml.llb/FindMatchingCloseTag.vi"/>
				<Item Name="Open_Create_Replace File.vi" Type="VI" URL="/&lt;vilib&gt;/_oldvers/_oldvers.llb/Open_Create_Replace File.vi"/>
				<Item Name="ParseXMLFragments.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/xml.llb/ParseXMLFragments.vi"/>
				<Item Name="Read From XML File(array).vi" Type="VI" URL="/&lt;vilib&gt;/Utility/xml.llb/Read From XML File(array).vi"/>
				<Item Name="Read From XML File(string).vi" Type="VI" URL="/&lt;vilib&gt;/Utility/xml.llb/Read From XML File(string).vi"/>
				<Item Name="Read From XML File.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/xml.llb/Read From XML File.vi"/>
				<Item Name="Error Cluster From Error Code.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Error Cluster From Error Code.vi"/>
			</Item>
			<Item Name="libahcutils.dll" Type="Document" URL="../CompactRio/libahcutils.dll"/>
			<Item Name="inpout32.dll" Type="Document" URL="/C/Windows/System32/inpout32.dll"/>
		</Item>
		<Item Name="Build Specifications" Type="Build">
			<Item Name="XWaveDriverNet" Type=".NET Interop Assembly">
				<Property Name="App_copyErrors" Type="Bool">true</Property>
				<Property Name="App_INI_aliasGUID" Type="Str">{3337336E-E48D-489B-B482-E1A72836AECD}</Property>
				<Property Name="App_INI_GUID" Type="Str">{E3E6E7F0-11F2-4499-9F6E-A06C0105FE22}</Property>
				<Property Name="App_serverConfig.httpPort" Type="Int">8002</Property>
				<Property Name="Bld_autoIncrement" Type="Bool">true</Property>
				<Property Name="Bld_buildCacheID" Type="Str">{F4428098-C457-4BD5-ADC7-E9F9D2CE51AD}</Property>
				<Property Name="Bld_buildSpecName" Type="Str">XWaveDriverNet</Property>
				<Property Name="Bld_excludeInlineSubVIs" Type="Bool">true</Property>
				<Property Name="Bld_excludeLibraryItems" Type="Bool">true</Property>
				<Property Name="Bld_excludePolymorphicVIs" Type="Bool">true</Property>
				<Property Name="Bld_localDestDir" Type="Path">../builds/NI_AB_PROJECTNAME/XWaveDriverNet</Property>
				<Property Name="Bld_localDestDirType" Type="Str">relativeToCommon</Property>
				<Property Name="Bld_modifyLibraryFile" Type="Bool">true</Property>
				<Property Name="Bld_previewCacheID" Type="Str">{9184BB74-64D8-4039-9396-45B996B39DED}</Property>
				<Property Name="Bld_version.build" Type="Int">33</Property>
				<Property Name="Bld_version.major" Type="Int">1</Property>
				<Property Name="Destination[0].destName" Type="Str">XWaveDriverNet.dll</Property>
				<Property Name="Destination[0].path" Type="Path">../builds/NI_AB_PROJECTNAME/XWaveDriverNet/XWaveDriverNet.dll</Property>
				<Property Name="Destination[0].preserveHierarchy" Type="Bool">true</Property>
				<Property Name="Destination[0].type" Type="Str">App</Property>
				<Property Name="Destination[1].destName" Type="Str">Support Directory</Property>
				<Property Name="Destination[1].path" Type="Path">../builds/NI_AB_PROJECTNAME/XWaveDriverNet/data</Property>
				<Property Name="DestinationCount" Type="Int">2</Property>
				<Property Name="DotNET2011CompatibilityMode" Type="Bool">false</Property>
				<Property Name="DotNETAssembly_ClassName" Type="Str">XWaveDriver</Property>
				<Property Name="DotNETAssembly_delayOSMsg" Type="Bool">true</Property>
				<Property Name="DotNETAssembly_Namespace" Type="Str">XWaveDv</Property>
				<Property Name="DotNETAssembly_signAssembly" Type="Bool">false</Property>
				<Property Name="DotNETAssembly_StrongNameKeyFileItemID" Type="Ref"></Property>
				<Property Name="DotNETAssembly_StrongNameKeyGUID" Type="Str">{1C18ABA6-6525-41A9-AF9C-031A1871E752}</Property>
				<Property Name="Source[0].itemID" Type="Str">{15557E2E-5E2C-495D-A353-64403EA96589}</Property>
				<Property Name="Source[0].type" Type="Str">Container</Property>
				<Property Name="Source[1].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[0]VIProtoConNum" Type="Int">3</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDataType" Type="Str">Bool</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[0]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[0]VIProtoName" Type="Str">returnvalue</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[0]VIProtoOutputIdx" Type="Int">3</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[1]VIProtoConNum" Type="Int">10</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDataType" Type="Str">String</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[1]VIProtoIutputIdx" Type="Int">10</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[1]VIProtoName" Type="Str">Filename_Config</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[1]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[2]VIProtoConNum" Type="Int">1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDataType" Type="Str">I32</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[2]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[2]VIProtoName" Type="Str">VarsCount</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[2]VIProtoOutputIdx" Type="Int">1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[3]VIProtoConNum" Type="Int">2</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[3]VIProtoDataType" Type="Str">Array</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[3]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[3]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[3]VIProtoName" Type="Str">VarTree</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[3]VIProtoOutputIdx" Type="Int">2</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[4]VIProtoConNum" Type="Int">3</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[4]VIProtoDataType" Type="Str">Bool</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[4]VIProtoDir" Type="Int">4</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[4]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[4]VIProtoName" Type="Str">Init__63</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[4]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[5]MethodName" Type="Str">Init</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[5]VIName" Type="Str">Init.vi</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[5]VIProtoConNum" Type="Int">0</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[5]VIProtoDataType" Type="Str">Cluster</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[5]VIProtoDir" Type="Int">7</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[5]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[5]VIProtoName" Type="Str">error__32out</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[5]VIProtoOutputIdx" Type="Int">0</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfoVIDocumentation" Type="Str">Initialize the VarTree from the XML File Config</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfoVIDocumentationEnabled" Type="Int">1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfoVIIsNamesSanitized" Type="Int">1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfoVIProtoItemCount" Type="Int">6</Property>
				<Property Name="Source[1].itemID" Type="Ref">/My Computer/Init.vi</Property>
				<Property Name="Source[1].sourceInclusion" Type="Str">TopLevel</Property>
				<Property Name="Source[1].type" Type="Str">ExportedAssemblyVI</Property>
				<Property Name="Source[10].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[10].itemID" Type="Ref">/My Computer/_init_vartree/typedefs/vartree_t.ctl</Property>
				<Property Name="Source[10].type" Type="Str">VI</Property>
				<Property Name="Source[11].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[11].itemID" Type="Ref">/My Computer/_init_vartree/typedefs/var_t.ctl</Property>
				<Property Name="Source[11].type" Type="Str">VI</Property>
				<Property Name="Source[12].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[12].itemID" Type="Ref">/My Computer/_init_vartree/typedefs/val_type_t.ctl</Property>
				<Property Name="Source[12].sourceInclusion" Type="Str">Include</Property>
				<Property Name="Source[12].type" Type="Str">VI</Property>
				<Property Name="Source[13].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[13].itemID" Type="Ref">/My Computer/_init_vartree/typedefs/ExVarTree.ctl</Property>
				<Property Name="Source[13].sourceInclusion" Type="Str">Include</Property>
				<Property Name="Source[13].type" Type="Str">VI</Property>
				<Property Name="Source[14].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[14].itemID" Type="Ref">/My Computer/_init_vartree/typedefs/ExVarTreeArray.ctl</Property>
				<Property Name="Source[14].sourceInclusion" Type="Str">Include</Property>
				<Property Name="Source[14].type" Type="Str">VI</Property>
				<Property Name="Source[15].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[0]VIProtoConNum" Type="Int">3</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDataType" Type="Str">Bool</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[0]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[0]VIProtoName" Type="Str">returnvalue</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[0]VIProtoOutputIdx" Type="Int">3</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[1]VIProtoConNum" Type="Int">11</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDataType" Type="Str">String</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[1]VIProtoIutputIdx" Type="Int">11</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[1]VIProtoName" Type="Str">In_Char</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[1]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[2]VIProtoConNum" Type="Int">9</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDataType" Type="Str">Array</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[2]VIProtoIutputIdx" Type="Int">9</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[2]VIProtoName" Type="Str">VarTree_in</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[2]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[3]VIProtoConNum" Type="Int">1</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[3]VIProtoDataType" Type="Str">Array</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[3]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[3]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[3]VIProtoName" Type="Str">VarTree_out</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[3]VIProtoOutputIdx" Type="Int">1</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[4]VIProtoConNum" Type="Int">3</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[4]VIProtoDataType" Type="Str">Bool</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[4]VIProtoDir" Type="Int">4</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[4]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[4]VIProtoName" Type="Str">Stat_Ok</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[4]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[5]MethodName" Type="Str">Unflatten</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[5]VIName" Type="Str">Unflatten.vi</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[5]VIProtoConNum" Type="Int">0</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[5]VIProtoDataType" Type="Str">Cluster</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[5]VIProtoDir" Type="Int">7</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[5]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[5]VIProtoName" Type="Str">error__32out</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfo[5]VIProtoOutputIdx" Type="Int">0</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfoVIDocumentation" Type="Str">Unflatten the character string to the tree variables.</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfoVIDocumentationEnabled" Type="Int">1</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfoVIIsNamesSanitized" Type="Int">1</Property>
				<Property Name="Source[15].ExportedAssemblyVI.VIProtoInfoVIProtoItemCount" Type="Int">6</Property>
				<Property Name="Source[15].itemID" Type="Ref">/My Computer/Unflatten.vi</Property>
				<Property Name="Source[15].sourceInclusion" Type="Str">TopLevel</Property>
				<Property Name="Source[15].type" Type="Str">ExportedAssemblyVI</Property>
				<Property Name="Source[2].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[0]VIProtoConNum" Type="Int">3</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDataType" Type="Str">Bool</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[0]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[0]VIProtoName" Type="Str">returnvalue</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[0]VIProtoOutputIdx" Type="Int">3</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[1]VIProtoConNum" Type="Int">9</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDataType" Type="Str">Cluster</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[1]VIProtoIutputIdx" Type="Int">9</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[1]VIProtoName" Type="Str">XwaveConnection</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[1]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[2]VIProtoConNum" Type="Int">2</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDataType" Type="Str">Cluster</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[2]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[2]VIProtoName" Type="Str">ConnsID</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[2]VIProtoOutputIdx" Type="Int">2</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[3]VIProtoConNum" Type="Int">0</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[3]VIProtoDataType" Type="Str">Cluster</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[3]VIProtoDir" Type="Int">4</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[3]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[3]VIProtoName" Type="Str">errorOut</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[3]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[4]MethodName" Type="Str">Connect</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[4]VIName" Type="Str">Connect.vi</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[4]VIProtoConNum" Type="Int">3</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[4]VIProtoDataType" Type="Str">Bool</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[4]VIProtoDir" Type="Int">4</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[4]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[4]VIProtoName" Type="Str">Connected</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[4]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfoVIDocumentation" Type="Str">Connect to the XWave device.</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfoVIDocumentationEnabled" Type="Int">1</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfoVIIsNamesSanitized" Type="Int">1</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfoVIProtoItemCount" Type="Int">5</Property>
				<Property Name="Source[2].itemID" Type="Ref"></Property>
				<Property Name="Source[2].sourceInclusion" Type="Str">TopLevel</Property>
				<Property Name="Source[2].type" Type="Str">ExportedAssemblyVI</Property>
				<Property Name="Source[3].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[3].itemID" Type="Ref">/My Computer/Vars_Init.vi</Property>
				<Property Name="Source[3].type" Type="Str">VI</Property>
				<Property Name="Source[4].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[0]VIProtoConNum" Type="Int">3</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDataType" Type="Str">Bool</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[0]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[0]VIProtoName" Type="Str">returnvalue</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[0]VIProtoOutputIdx" Type="Int">3</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[1]VIProtoConNum" Type="Int">11</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDataType" Type="Str">Cluster</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[1]VIProtoIutputIdx" Type="Int">11</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[1]VIProtoName" Type="Str">Conn</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[1]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[2]VIProtoConNum" Type="Int">10</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDataType" Type="Str">I32</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[2]VIProtoIutputIdx" Type="Int">10</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[2]VIProtoName" Type="Str">InputsRate</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[2]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[3]VIProtoConNum" Type="Int">9</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[3]VIProtoDataType" Type="Str">Array</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[3]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[3]VIProtoIutputIdx" Type="Int">9</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[3]VIProtoName" Type="Str">VarTree_in</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[3]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[4]VIProtoConNum" Type="Int">2</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[4]VIProtoDataType" Type="Str">Cluster</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[4]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[4]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[4]VIProtoName" Type="Str">DataStats</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[4]VIProtoOutputIdx" Type="Int">2</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[5]VIProtoConNum" Type="Int">1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[5]VIProtoDataType" Type="Str">Array</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[5]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[5]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[5]VIProtoName" Type="Str">VarTree_out</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[5]VIProtoOutputIdx" Type="Int">1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[6]VIProtoConNum" Type="Int">3</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[6]VIProtoDataType" Type="Str">Bool</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[6]VIProtoDir" Type="Int">4</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[6]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[6]VIProtoName" Type="Str">Conn_Problem__63</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[6]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[7]MethodName" Type="Str">GetData</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[7]VIName" Type="Str">GetData.vi</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[7]VIProtoConNum" Type="Int">0</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[7]VIProtoDataType" Type="Str">Cluster</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[7]VIProtoDir" Type="Int">7</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[7]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[7]VIProtoName" Type="Str">error__32out</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[7]VIProtoOutputIdx" Type="Int">0</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfoVIDocumentation" Type="Str">Get data from the XWave device.</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfoVIDocumentationEnabled" Type="Int">1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfoVIIsNamesSanitized" Type="Int">1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfoVIProtoItemCount" Type="Int">8</Property>
				<Property Name="Source[4].itemID" Type="Ref"></Property>
				<Property Name="Source[4].properties[0].type" Type="Str">Remove front panel</Property>
				<Property Name="Source[4].properties[0].value" Type="Bool">false</Property>
				<Property Name="Source[4].properties[1].type" Type="Str">Remove block diagram</Property>
				<Property Name="Source[4].properties[1].value" Type="Bool">true</Property>
				<Property Name="Source[4].propertiesCount" Type="Int">2</Property>
				<Property Name="Source[4].sourceInclusion" Type="Str">TopLevel</Property>
				<Property Name="Source[4].type" Type="Str">ExportedAssemblyVI</Property>
				<Property Name="Source[5].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[0]VIProtoConNum" Type="Int">-1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDataType" Type="Str">void</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[0]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[0]VIProtoName" Type="Str">returnvalue</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[0]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[1]MethodName" Type="Str">CloseConn</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[1]VIName" Type="Str">Close_conn.vi</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[1]VIProtoConNum" Type="Int">10</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDataType" Type="Str">Cluster</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[1]VIProtoIutputIdx" Type="Int">10</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[1]VIProtoName" Type="Str">ConnsID</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[1]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfoVIDocumentation" Type="Str">Disconnect from the XWave device.</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfoVIDocumentationEnabled" Type="Int">1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfoVIIsNamesSanitized" Type="Int">1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfoVIProtoItemCount" Type="Int">2</Property>
				<Property Name="Source[5].itemID" Type="Ref"></Property>
				<Property Name="Source[5].sourceInclusion" Type="Str">TopLevel</Property>
				<Property Name="Source[5].type" Type="Str">ExportedAssemblyVI</Property>
				<Property Name="Source[6].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[6].itemID" Type="Ref"></Property>
				<Property Name="Source[6].type" Type="Str">VI</Property>
				<Property Name="Source[7].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[7].itemID" Type="Ref">/My Computer/Conns_Params.ctl</Property>
				<Property Name="Source[7].sourceInclusion" Type="Str">Include</Property>
				<Property Name="Source[7].type" Type="Str">VI</Property>
				<Property Name="Source[8].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[8].itemID" Type="Ref">/My Computer/DataCluster.ctl</Property>
				<Property Name="Source[8].sourceInclusion" Type="Str">Include</Property>
				<Property Name="Source[8].type" Type="Str">VI</Property>
				<Property Name="Source[9].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[9].itemID" Type="Ref">/My Computer/ConsCluster.ctl</Property>
				<Property Name="Source[9].sourceInclusion" Type="Str">Include</Property>
				<Property Name="Source[9].type" Type="Str">VI</Property>
				<Property Name="SourceCount" Type="Int">16</Property>
				<Property Name="TgtF_companyName" Type="Str">MEC Ltd.</Property>
				<Property Name="TgtF_fileDescription" Type="Str">XWaveDriverNet</Property>
				<Property Name="TgtF_internalName" Type="Str">XWaveDriverNet</Property>
				<Property Name="TgtF_legalCopyright" Type="Str">Copyright © 2015 MEC Ltd.</Property>
				<Property Name="TgtF_productName" Type="Str">XWaveDriverNet</Property>
				<Property Name="TgtF_targetfileGUID" Type="Str">{6D65196B-2605-4795-A11C-D4610F39FD2F}</Property>
				<Property Name="TgtF_targetfileName" Type="Str">XWaveDriverNet.dll</Property>
			</Item>
		</Item>
	</Item>
</Project>
