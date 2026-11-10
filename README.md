# LinkChain

A C# console project about **linked lists**. It is built around a generic `Node<T>` class and includes a library of list algorithms (iterative and recursive), circular lists, chains with cycles (tortoise and hare), a copy constructor, and a few "real-world" exercises (marathon runners, cards, perfumes) that use linked lists as their data structure.

## Project Structure

```
LinkChain/
├── Node.cs          # Generic Node<T> class + copy constructor
├── NodeUtils.cs     # List algorithms (build, print, sum, sort, merge, reverse...)
├── NodesCircles.cs  # Circular linked list operations
├── ChainCycles.cs   # Chains with cycles - Floyd's tortoise and hare
├── TestUtils.cs     # Tests for the NodeUtils methods
├── Program.cs       # Main + test runners for every part of the project
├── Card.cs          # Card (number + color)
├── ItemMrtn.cs      # One marathon result (year + score)
├── MrtnRunner.cs    # A runner and a list of his results
├── MrtnRace.cs      # A race and a list of its runners
├── Perfume.cs       # A perfume and a list of its smells
└── Smell.cs         # One smell (essence, intensity, concentration)
```

## The `Node<T>` Class

| Member | Description |
| --- | --- |
| `Node(T value)` | Creates a node with no next node |
| `Node(T value, Node<T> next)` | Creates a node pointing to `next` |
| `Node(Node<T> node)` | **Copy constructor** - copies the whole chain |
| `GetValue()` / `SetValue(T)` | Read / change the value |
| `GetNext()` / `SetNext(Node<T>)` | Read / change the next node |
| `HasNext()` | `true` if there is a next node |

### Copy Constructor ("Constructive Copying Action")

`new Node<T>(node)` copies the node **and every node after it**. The new chain shares no nodes with the original, so changing one never affects the other.

```csharp
Node<int> original = NodeUtils.CreateListFromArray(new int[] { 4, 5, 10 });
Node<int> copy = new Node<int>(original);

copy.SetValue(1);
Console.WriteLine(original.GetValue()); // still 4
```

## What's Inside

### 1. List utilities - `NodeUtils.cs`
Most exercises have an iterative version and a recursive version (the recursive one ends with `R`, e.g. `SumList` / `SumListR`).

- **Build / print:** `CreateListFromArray`, `PrintList`
- **Compare / count / sum:** `CompareList`, `CountList`, `SumList`
- **Search:** `IsExist`, `MaxList`, `GetElement`
- **Check:** `IsSorted`, `IsBalanced`
- **Change:** `MakePositive`, `RemoveDuplicates`, `RemoveMax`, `RemoveMiddle`, `RemoveCommon`, `InsertAfterValue`
- **Sorted lists:** `Insert2SortedList`, `SortList`, `MergeLists`, `Intersection`
- **Other:** `ReverseList`, `CountSequences`, `MaxNegativeSeq`, `PrintBetween`

### 2. Circular lists - `NodesCircles.cs`
`BuildCircle`, `MakeCircular`, `BreakCircle`, `PrintCircle`, `IsCircleChain`, `CountCircleChain`, `SumCircle`, `ContainsCircle`, `AddFirst`, `AddLast`, `AddSumsBetween`, `AddBeforeEven`, `RemoveFirst`, `RemoveLast`, `RemoveEvens`

### 3. Chains with cycles - `ChainCycles.cs`
A chain that may close into a cycle (a tail followed by a loop). All detection uses **Floyd's tortoise and hare** - O(n) time, O(1) extra space, comparing nodes by reference and never by value.

- **Build helpers:** `BuildChain(n)` builds 1->2->...->n, `MakeCycle(chain, k)` connects the last node back to node k
- **Cycle detection:** `HasCycle` (List-based) and `HasCycleFast` (tortoise and hare)
- **Measurements:** `CycleStart` (first node of the cycle), `TailLength`, `CycleLength`, `Length` (distinct nodes)
- **Print / fix:** `PrintChain` prints the cycle in parentheses, `BreakCycle` turns the chain linear
- **Challenge:** `Middle` finds the middle node of a linear chain in one pass

### 4. Marathon - `MrtnRace`, `MrtnRunner`, `ItemMrtn`
A race holds a list of runners, and each runner holds a list of results. `AddResult` adds a result to a runner by ID, and `CountConsecutiveRunners` counts runners who have run every year since they started (at least 3 years).

### 5. Cards - `Card`
`GroupCards` rearranges a list of cards so cards with the same number are next to each other.

### 6. Perfumes - `Perfume`, `Smell`
A perfume holds a list of smells. `Is100Percent` checks that the concentrations add up to 100, and `CommonSmells` builds a new list of smells that appear in both perfumes.

## Running the Project

Open the project in **Visual Studio** (it is a .NET Framework console app) and run it.

In `Program.cs`, `Main` has one line per part. Un-comment the one you want to run:

```csharp
static void Main(string[] args)
{
    TestChainCycles();    // chains with cycles
    //TestUtils.RunAll();   // list utilities
    //TestPerfume();        // perfumes
    //TestGroupCards();     // cards
    //TestMarathon();       // marathon
    //Test_Node();          // copy constructor
    //TestCircular();       // circular lists
}
```

`TestChainCycles()` runs the full test table for cycle chains: 7 chains covering every case (cycle in the middle, linear, cycle at the first node, self-loop, single node, empty), plus `BreakCycle` and `Middle` on chains of length 1, 2, 5 and 6. Each line prints the result next to the expected one.

### Copy constructor tests
`Test_Node()` prints `OK` for every check that passes. It checks that the copy has the same values, a different head, and no shared nodes, that the copy and the original don't affect each other, and that copying a single node and copying a copy both work.

## Author

Eitan
