from pathlib import Path

def read_file(file: str) -> str:
    with open(file, 'r') as f:
        data = f.read()

    return data

def read_lines(file: str) -> list[str]:
    # Read all lines in file into raw collection
    raw = read_file(file)
    
    # Split into lines, strip each line, and filter out empty lines
    cleaned = [line.strip() for line in raw.splitlines() if line.strip()]

    return cleaned


def load_input(day_num: int, year: int = 2025) -> list[str]:
    """
    Load the problem input for a given day and year.
    
    Args:
        day_num: The day number (1-25)
        year: The year (default: 2025)
    
    Returns:
        The contents of the input file as a string
    """
    input_dir = Path(rf"C:\_test\advent\{year}")
    input_file = input_dir / f"day-{day_num:02d}.txt"
    
    return read_lines(input_file)


def load_test(day_num: int, year: int = 2025) -> list[str]:
    """
    Load the test input for a given day and year.
    
    Args:
        day_num: The day number (1-25)
        year: The year (default: 2025)
    
    Returns:
        The contents of the test input file as a string
    """
    input_dir = Path(rf"C:\_test\advent\{year}")
    input_file = input_dir / f"day-{day_num:02d}-test.txt"
    
    return read_lines(input_file)