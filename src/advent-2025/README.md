# Advent of Code 2025

Python solutions for Advent of Code 2025.

## Utility Functions

The `util` module provides helper functions for loading input data and working with files.

### Input Loading

Input files are stored in `C:\_test\advent\2025\` and are not included in source control.

- **Problem input**: `day-01.txt`, `day-02.txt`, etc.
- **Test input**: `day-01-test.txt`, `day-02-test.txt`, etc.

#### `load_input(day_num: int, year: int = 2025) -> str`

Loads the problem input for a given day.

```python
from util import load_input

# Load day 1 input
data = load_input(1)

# Load day 5 input for a different year
data = load_input(5, year=2023)
```

#### `load_test(day_num: int, year: int = 2025) -> str`

Loads the test input for a given day.

```python
from util import load_test

# Load day 1 test input
test_data = load_test(1)
```

### File Reading

Additional utility functions for reading files:

#### `read_file(file: str) -> str`

Reads an entire file and returns its contents as a string.

#### `read_lines(file: str) -> list[str]`

Reads a file line by line, strips whitespace, and returns a list of cleaned lines.

## Usage Example

```python
from util import load_input, load_test

# Load test input for development
test_data = load_test(1)
# ... test your solution ...

# Load actual problem input
data = load_input(1)
# ... solve the problem ...
```
