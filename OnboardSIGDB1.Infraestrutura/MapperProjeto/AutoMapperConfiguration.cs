using System;
using System.Collections.Generic;
using AutoMapper;

namespace OnboardSIGDB1.Infraestrutura.MapperProjeto
{
    public static class AutoMapperConfiguration
    {
        private static object _thisLock = new object();
        private static bool _initialized = false;

        public static IEnumerable<Type> GetAutoMapperProfiles()
        {
            var result = new List<Type>
            {
                typeof(OnboardMapper),
            };
            return result;
        }

        public static void Initialize()
        {
            lock (_thisLock)
            {
                if (!_initialized)
                {
                    Mapper.Reset();
                    Mapper.Initialize((cfg) =>
                    {
                        cfg.AddProfiles(GetAutoMapperProfiles());
                    });
                    _initialized = true;
                }
            }
        }

    }
}
