using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Castle.Core.Internal;

[assembly: AssemblyTitle("Business.Tests")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Business.Tests")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]

[assembly: Guid("d84a4224-7219-4a66-9ef0-dfb39edd99b9")]

// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Make internals visible to Moq.
[assembly: InternalsVisibleTo(InternalsVisible.ToCastleCore)]
[assembly: InternalsVisibleTo(InternalsVisible.ToDynamicProxyGenAssembly2)]
//[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]