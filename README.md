# LinkChain

A C# console project about **linked lists**. It is built around a generic `Node<T>` class and includes a library of list algorithms (iterative and recursive), circular lists, chains with cycles (tortoise and hare), a copy constructor, polynomial operations, and a few "real-world" exercises (marathon runners, cards, perfumes) that use linked lists as their data structure.

## Project Structure

```
LinkChain/
├── Node.cs          # Generic Node<T> class + copy constructor
├── NodeUtils.cs     # List algorithms (build, print, sum, sort, merge, reverse...)
├── NodesCircles.cs  # Circular linked list operations
├── ChainCycles.cs   # Chains with cycles - Floyd's tortoise and hare
├── Monomial.cs      # One polynomial term: factor and exponent
├── Polynomial.cs    # Sort, reduce, format, evaluate and add polynomials
├── PolynomialTests.cs # Polynomial checks and reference-model tests
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

`new Node<T>(node)` copies the node **and every node after it**. The new chain shares no nodes with the original. Values are copied by assignment: for reference-type values, the two chains still refer to the same value objects. Replacing a node value or changing a link in one chain does not change the other chain, but changing a shared value object can affect both.

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
A chain that may close into a cycle (a tail followed by a loop). `HasCycleFast` uses **Floyd's tortoise and hare**, with O(n) time and O(1) extra space. `HasCycle` instead keeps a list of visited node references. Both distinguish nodes by reference, not by stored value.

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

### 7. Polynomials - `Monomial.cs`, `Polynomial.cs`, `PolynomialTests.cs`
A polynomial is a `Node<Monomial>` chain. Each term stores an integer `factor` and a non-negative integer `exponent`.

- `Monomial` provides a constructor, getters/setters, and signed formatting such as `+5x^4`, `-x` or `+3`. A zero factor formats as `+0`.
- `PolynomialSort` sorts terms by descending exponent without combining or deleting them. It swaps complete Monomial values between existing nodes.
- `ReduceSortedPolynomial` combines adjacent equal exponents in an already sorted chain and removes terms whose factor is zero. It can change the head.
- `PolynomialToString` formats terms in their existing order with the prefix `f(x)=`. It does not sort or reduce. An empty chain formats as `f(x)=0`.
- `PrintPolynomial` prints the formatted polynomial followed by a newline.
- `CalcPolynomial` evaluates all terms at an integer x, using `Math.Pow`. An empty chain evaluates to 0.
- `AddPolynomial` sorts and reduces both inputs, then combines their terms in descending order, omitting zero sums.

Sorting, reduction and addition may change their input chains. Always use the returned head after these operations.

The assignment's input bounds are `|factor| <= 10`, `0 <= exponent <= 6`, `|x| <= 10` and at most 50 terms per input chain.

#### Example

```csharp
Monomial[] terms = {
    new Monomial(5, 4),
    new Monomial(1, 4),
    new Monomial(3, 1),
    new Monomial(5, 0)
};
Node<Monomial> poly = NodeUtils.CreateListFromArray(terms);
poly = Polynomial.PolynomialSort(poly);
poly = Polynomial.ReduceSortedPolynomial(poly);
Polynomial.PrintPolynomial(poly); // f(x)=+6x^4+3x+5
Console.WriteLine(Polynomial.CalcPolynomial(poly, 3)); // 500
```

#### Complexity

Here n and m are the input chain lengths. Extra working space excludes returned output nodes.

| Operation | Time | Extra working space |
| --- | --- | --- |
| PolynomialSort | O(n²), including already sorted input | O(1) |
| ReduceSortedPolynomial | O(n) | O(1) |
| AddPolynomial, including its sorting steps | O(n² + m²) | O(1), plus up to O(n+m) output space |

`PolynomialTests.RunAll()` runs checks for formatting, getters/setters, sorting, reduction, evaluation and addition. It includes deterministic examples, generated inputs and some output-reference checks. A passing summary is useful evidence, not a guarantee that every possible property was tested.

## Running the Project

The repository currently contains source files but no `.sln` or `.csproj`. Use the existing local LinkChain project in Visual Studio, or add the source files to a compatible C# Console App project. When creating a new project, replace its generated entry-point code with this project's `Program.cs` rather than keeping two entry points. Some existing files use .NET Framework-specific namespaces, so a .NET Framework Console App is the closest match for the current sources.

The current `Main` runs the polynomial tests:

```csharp
static void Main(string[] args)
{
    PolynomialTests.RunAll();
}
```

Older test calls remain commented out in `Program.cs`. To work on another section, disable the polynomial call and enable the corresponding older test call, such as `TestChainCycles()` or `TestUtils.RunAll()`.

`TestChainCycles()` covers linear and cyclic chains, cycle measurements, breaking cycles and finding a middle node.

### Copy constructor tests
`Test_Node()` prints `OK` for every check that passes. It checks that the copy has the same values, a different head, and no shared nodes, that the copy and the original don't affect each other, and that copying a single node and copying a copy both work.

## Author

Eitan
