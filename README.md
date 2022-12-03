# Advent

- [Advent](#advent)
    - [Overview](#overview)
- [Architecture](#architecture)
- [Usage](#usage)
    - [Configuring Input Data](#configuring-input-data)

## Overview
`Advent` is intended to house all ongoing development effort for the yearly [Advent of Code](https://adventofcode.com/) challenges created by [Eric Wastl](http://was.tl/).  

Originally, these were in separate repos but it seems much tidier having them all together.

# Architecture

The repository structure follows a simple pattern of a root `src/Advent.sln` file that contains all the yearly source and the test project.  

Each year is added as a single project which depends upon `src/Advent.Domain` which houses all the year-agnostic helper functionality.  

All of the actual source is organized as follows:
- [/src](/src/)
    - [Advent.2020](/src/Advent.2020/)
    - [Advent.2021](/src/Advent.2021/)
    - [Advent.2022](/src/Advent.2022/)
    - [Advent.Domain](/src/Advent.Domain/)

While all of the (daily) unit testing is stored within a single project here:
- [/tests/Advent.Tests](/tests/Advent.Tests/)

# Usage

In order to use this project - simply clone the source locally via a single:
```shell
git clone https://github.com/gclodge/Advent.git
```

Then, navigate into the source directory via:
```shell
cd Advent
```

## Configuring Input Data

In order for `Advent.Tests` to find the daily input & test data without storing it in the repo itself - the path to the input directory is stored in the `ADVENT_INPUT_DIR_ROOT` environment variable.

The test project expects data to be organized into yearly directories and then each days input is broken up into a `Day.XX.Input.txt` or `Day.XX.Test.txt` file.

Each daily test extends the `IDailyTest` interface and as such will have associated `Year` and `Day` fields.  So an `IDailyTest` with a year of `2022` and day of `3` would be looking for the following:

- `ADVENT_INPUT_DIR_ROOT/2022/Day.03.Input.txt`
