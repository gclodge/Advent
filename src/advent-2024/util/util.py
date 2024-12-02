
def read_file(file: str) -> str:
    with open(file, 'r') as f:
        data = f.read()

    return data

def read_lines(file: str) -> list[str]:
    # Read all lines in file into raw collection
    with open(file, 'r') as f:
        raw = f.readlines()
    
    # Strip each line to remove leading/trailing spaces, and new lines
    cleaned = [line.strip() for line in raw]

    return cleaned