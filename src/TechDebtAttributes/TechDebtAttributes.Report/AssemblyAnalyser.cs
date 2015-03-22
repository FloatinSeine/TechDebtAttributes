using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TechDebtAttributes.Report.Configuration;

namespace TechDebtAttributes.Report
{
    public class AssemblyAnalyser
    {
        private readonly string _scanFolder;

        public AssemblyAnalyser(ReportConfigurationElement config) : this(config.FolderToScan)
        {
        }

        public AssemblyAnalyser(string config)
        {
            _scanFolder = config;
        }

        public IEnumerable<MemberInfo> Analyse()
        {
            var assemblies = GetAssemblies(_scanFolder);
            var types = FindTypesWithCleanCodeAttributes(assemblies);

            return types;
        }

        private static IEnumerable<MemberInfo> FindTypesWithCleanCodeAttributes(IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(FindAllTheTypesThatHaveCleanCodeAttributes);
        }

        private static IEnumerable<MemberInfo> FindAllTheTypesThatHaveCleanCodeAttributes(Assembly assembly)
        {
            return assembly.GetTypes()
                .SelectMany(type => type.GetMembers())
                .Union(assembly.GetTypes())
                .Where(type => Attribute.IsDefined(type, typeof(CleanCodeAttribute)));
        }

        private static IEnumerable<Assembly> GetAssemblies(string path)
        {
            var files = FetchAssemblyFileList(path);

            var list = new List<Assembly>(files.Length);
            list.AddRange(files.Select(GetAssembly));

            return list.TakeWhile(a => a != null);
        }

        private static string[] FetchAssemblyFileList(string rootPath)
        {
            return (!Directory.Exists(rootPath) ? null : Directory.GetFiles(rootPath, "*.dll"));
        }

        private static Assembly GetAssembly(string path)
        {
            return path.EndsWith("TechDebtAttributes.dll") ? null : Assembly.LoadFrom(path);
        }
    }
}
