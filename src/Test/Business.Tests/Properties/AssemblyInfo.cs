using System.Runtime.CompilerServices;

using Castle.Core.Internal;

// Make internals visible to Moq.
[assembly: InternalsVisibleTo(InternalsVisible.ToCastleCore)]
[assembly: InternalsVisibleTo(InternalsVisible.ToDynamicProxyGenAssembly2)]
//[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]