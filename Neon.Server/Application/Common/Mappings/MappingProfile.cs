using System.Reflection;
using AutoMapper;

namespace Neon.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly( Assembly.GetExecutingAssembly() );
    }

    private void ApplyMappingsFromAssembly( Assembly assembly )
    {
        void Apply( Assembly assembly, Type interfaceType, string interfaceName )
        {
            var types = assembly.GetExportedTypes()
                .Where( t => t.GetInterfaces().Any( i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType ) )
                .ToList();

            foreach( var type in types )
            {
                var instance = Activator.CreateInstance( type );

                var methodInfo = type.GetMethod( "Mapping" ) ?? type.GetInterface( interfaceName )!.GetMethod( "Mapping" );

                methodInfo?.Invoke( instance, new object[] { this } );
            }
        }

        Apply( assembly, typeof( IMapFrom<> ), "IMapFrom`1" );
        Apply( assembly, typeof( IMapTo<> ), "IMapTo`1" );
    }    
}