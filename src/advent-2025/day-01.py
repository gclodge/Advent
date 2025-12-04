from util import load_input, load_test

def parse_step(step: str) -> tuple[str, int]:
    letter = step[0]
    number = int(step[1:])
    return letter, number

def calculate_final_dial(steps: list[str], is_part_two: bool = False) -> tuple[int, int]:
    dial_value: int = 50
    prev_value: int = dial_value
    zero_count: int = 0

    print(f"{'Part 2' if is_part_two else 'Part 1'} Calculation Starting...")

    for step in steps:
        prev_value = dial_value
        dir, amount = parse_step(step)

        # If the rotation value is 100 or more, wrap around
        if amount >= 100:
            # Get the multiple of 100s removed, to add as 'hits' to zero count
            if is_part_two:
                zero_count += amount // 100

            amount = amount % 100

        # Apply the directionality of the step
        if dir == 'L':
            dial_value -= amount    
        elif dir == 'R':
            dial_value += amount

        # If dial goes below 0, wrap around to 100
        if dial_value < 0:
            dial_value += 100
            if is_part_two and prev_value != 0:
                zero_count += 1
                continue
        elif dial_value >= 100:
            dial_value -= 100
            if is_part_two and prev_value != 0:
                zero_count += 1
                continue
        
        if dial_value == 0:
            zero_count += 1

    return dial_value, zero_count

steps = load_input(1)

dial_value, zero_count = calculate_final_dial(steps)

print(' -- Part 1 Results -- ')
print(f"Final dial value: {dial_value}")
print(f"Password value: {zero_count}")

dial_value, zero_count = calculate_final_dial(steps, is_part_two=True)

print(' -- Part 2 Results -- ')
print(f"Final dial value: {dial_value}")
print(f"Password value: {zero_count}")