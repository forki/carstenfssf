namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("DAG")>]
[<assembly: AssemblyProductAttribute("carstenfssf")>]
[<assembly: AssemblyDescriptionAttribute("Carsten Jorgensen FSSF")>]
[<assembly: AssemblyVersionAttribute("1.0")>]
[<assembly: AssemblyFileVersionAttribute("1.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "1.0"
