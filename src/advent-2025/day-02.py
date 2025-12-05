import sys
import re
from util import load_input, load_test

class PasswordRange:
    def __init__(self, range_str: str) -> None:
        parts = range_str.split('-')
        #print(f"{range_str} -> {parts}")
        self.min = int(parts[0])
        self.max = int(parts[1])

    def find_invalid_passwords(self, part_two: bool = False) -> list[int]:
        invalid_passwords = []
        for password in range(self.min, self.max + 1):
            # Remove the leading zeroes via string conversion
            password_str = str(password)
            
            # Check if the number is composed of a digit pattern repeated exactly twice
            # Pattern explanation: ^(\d+)\1$ matches a capturing group repeated exactly once more (twice total)
            # The pattern must be exactly half the length of the string
            is_repeated_twice: bool = False
            
            if part_two:
                is_repeated_twice = bool(re.match(r'^(\d+?)\1+$', password_str))
            else:
                is_repeated_twice = bool(re.match(r'^(\d+)\1$', password_str))
            
            if not is_repeated_twice: continue

            invalid_passwords.append(password)

        return invalid_passwords

    def contains(self, value: int) -> bool:
        return self.min <= value <= self.max

def main(data: list[str] | None = None) -> None:
    ranges : list[PasswordRange] = []
    for line in data or []:
        for range in line.split(','):
            if len(range) > 0:
                ranges.append(PasswordRange(range.strip()))

    part_one: list[int] = []
    for range in ranges:
        part_one.extend(range.find_invalid_passwords())

    print(f'Part One: {sum(part_one)}')

    part_two: list[int] = []
    for range in ranges:
        part_two.extend(range.find_invalid_passwords(part_two=True))

    print(f'Part Two: {sum(part_two)}')

    return

if __name__ == "__main__":
    if "--test" in sys.argv:
        data = load_test(2)
    else:
        data = load_input(2)

    main(data)