open System.Text

type DiGraph<'n> when 'n : comparison = Map<'n,  List<'n>>

type Edge<'n> = 'n * 'n

let empty = Map.empty<'n, List<'n>>

let addNode (n : 'n) (g : DiGraph<'n>) : DiGraph<'n> = 
    match Map.tryFind n g with
    | None -> g.Add(n, List.empty<'n>)
    | Some _ -> g

let addEdge ((n1, n2): ('n * 'n)) (g: DiGraph<'n>) : DiGraph<'n> =
    let h = 
      match Map.tryFind n2 g with
      | None -> addNode n2 g
      | Some _ -> g
    match Map.tryFind n1 g with
    | None -> Map.add n1 [n2] h
    | Some ns -> Map.add n1 (List.append [n2] ns) h

let buildGraph (edges : Edge<'n> list) : DiGraph<'n> = 
    let rec builder (e : Edge<'n> list) (g : DiGraph<'n>) = 
        match e with
        | [] -> g
        | e :: es -> builder es (addEdge e g)
    
    let empty = Map.empty<'n, List<'n>>
    builder edges empty

let nodes (g: DiGraph<'n>) : List<'n> =
    Map.fold (fun xs k v -> k::xs) [] g

let adjList (n : 'n) (g : DiGraph<'n>) : List<'n> =
    Map.find n g

let edges (g : DiGraph<'n>) : Edge<'n> list =
    let n = Map.fold (fun xs k v -> k::xs) [] g
    let adL = Map.fold (fun xs k v -> v::xs) [] g
    seq { 
        for v in nodes g do
            for w in (adjList v g) -> v, w
    }
    |> List.ofSeq

let print (g : DiGraph<'n>) = 
        let sb = StringBuilder()
        sb.Append("digraph {\n") |> ignore
        let edg = edges g
        edg 
        |> List.iter (fun a -> (sb.AppendFormat("{0} -> {1}\n", fst a, snd a) |> ignore))
        sb.Append("}") |> ignore
        sb.ToString()

let adjacencyNodes (n : 'n) (g : DiGraph<'n>) : bool = 
    let adL = Map.fold (fun xs k v -> v::xs) [] g
    adL |> List.concat |> List.contains n

let rootNodes (g : DiGraph<'n>) : List<'n> = 
    let allNodes = nodes g
    List.filter (fun n -> not (adjacencyNodes n g)) allNodes

let topoSort (h : DiGraph<'n>) : List<'n> = 
    let rec dfs (g : DiGraph<'n>, order : List<'n>, rts : List<'n>) : List<'n> = 
        if List.isEmpty rts then order
        else 
            let n = List.head rts
            let order' = n :: order
            let g' = Map.remove n g
            let rts' = rootNodes g'
            dfs (g', order', rts')
    List.rev (dfs (h, [], rootNodes h))

type TopologicalSort = | Cyclic | NoCyclic

let directedAcyclicGraph (g : DiGraph<'n>) : TopologicalSort =
    match topoSort g with
    | [] -> Cyclic
    | _ -> NoCyclic

let b = [(1, 2); (1, 4); (2, 3); (4, 6); (5, 8); (6, 5); (6, 8)]

let c = [(1, 2); (2, 3); (1, 3); (2, 4); (2, 5)]

let d = [(0, 1); (0, 2); (1, 2); (1, 6); (1, 5); (2, 4); (2, 6); (5, 7); (5, 6); (6, 7)]

let a = [(1, 2); (1, 3); (2, 4); (2, 5)]
let g = buildGraph a
let s = topoSort g

