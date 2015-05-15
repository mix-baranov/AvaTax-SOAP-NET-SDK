#region Copyright
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyCompany("Avalara")]
[assembly: AssemblyProduct("AvaTax")]
[assembly: AssemblyCopyright("© 2004 - 2011 Avalara, Inc.  All rights reserved.")]
[assembly: AssemblyTrademark("Avalara")]
[assembly: AssemblyCulture("")]


[assembly: AssemblyVersion("14.4.2.0")]
[assembly: AllowPartiallyTrustedCallers()]



//changing this value to true would have a detrimental effect on the Adapter;
//  if it must be true, then we need to move this attribute from here to each local AssemblyInfo.cs
[assembly: ComVisible(false)]

[assembly: CLSCompliant(true)]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

//[assembly: AssemblyVersion("4.0.0.*")]

//Design Guidelines for Exposing Functionality COM recommends this attribute be set to false,
//    in effect setting the attribute for all classes to false. Explicit ComVisible attributes
//    should be set at the class level.
//[assembly: ComVisible(false)]

//The following GUID is for the ID of the typelib if this project is exposed to COM
//change this every time you want to release it to get rid of DLL HELL
[assembly: Guid("7e148de8-9063-49d6-ac25-77ac86eb4dbc")]

//Necessary from some COM clients in order to access the class members.
[assembly: ClassInterface(ClassInterfaceType.AutoDual)]

//
// In order to sign your assembly you must specify a key to use. Refer to the 
// Microsoft .NET Framework documentation for more information on assembly signing.
//
// Use the attributes below to control which key is used for signing. 
//
// Notes: 
//   (*) If no key is specified, the assembly is not signed.
//   (*) KeyName refers to a key that has been installed in the Crypto Service
//       Provider (CSP) on your machine. KeyFile refers to a file which contains
//       a key.
//   (*) If the KeyFile and the KeyName values are both specified, the 
//       following processing occurs:
//       (1) If the KeyName can be found in the CSP, that key is used.
//       (2) If the KeyName does not exist and the KeyFile does exist, the key 
//           in the KeyFile is installed into the CSP and used.
//   (*) In order to create a KeyFile, you can use the sn.exe (Strong Name) utility.
//       When specifying the KeyFile, the location of the KeyFile should be
//       relative to the project output directory which is
//       %Project Directory%\obj\<configuration>. For example, if your KeyFile is
//       located in the project directory, you would specify the AssemblyKeyFile 
//       attribute as [assembly: AssemblyKeyFile("..\\..\\mykey.snk")]
//   (*) Delay Signing is an advanced option - see the Microsoft .NET Framework
//       documentation for more information on this.


[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyName("")]



