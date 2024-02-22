# Development guide

## Code style

We follow the [Microsoft C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
to both demonstrate best C# practices and ensure a consistent look to the code.

## Commits

It is important to follow a few commit message conventions to ensure a uniform
commit history. We follow the [Conventional Commits specification](https://www.conventionalcommits.org/en/v1.0.0/).

Since we squash our commits from feature branches (see [Branching strategy](#branching-strategy)),
you can follow the commit style conventions for the PR title only. This means
that your individual commits on a feature branch do **not** need to follow the
commit conventions as long as your PR title does.

### Examples

- `feat: create scenes for game`
- `docs: correct spelling in README.md`
- `fix: prevent racing of requests`

## Issues

- Trello will be used to track a bug or feature
- Each Trello card can represent one or more feature branches
- Assign issues to yourself when working on it to minimise redundancy

## Sprints

- Our sprints are 1 week long
- All the work for each sprint will be tagged by the sprint number on the Trello
- Uncompleted issues for a given sprint will be moved to the following sprint
- At the end of every sprint, the `dev` branch is merged back into `master`

## Branches

### Policies

The `master` and `dev` branches have a few policies setup through GitHub
- Cannot be pushed to directly
- Requires a pull request before merging
- Pull requests require at least 1 approval before merging

### Branching strategy

> [Microsoft Git branching strategy](https://learn.microsoft.com/en-us/azure/devops/repos/git/git-branching-guidance)

![](https://user-images.githubusercontent.com/59267627/199492032-00ffa95f-4958-40bb-a10c-01a7e5ba8171.png)

- Use feature branches derived from the latest copy of `dev` for all new features/bugfixes (run `git pull`)
- Merge feature branches into the `dev` branch during sprints
- Commits on a feature branch will be squashed during merge to maintain a linear history

#### Naming

Use a consistent naming convention for your feature branches. This will help
identify the work done in the branch. Our branch naming conventions is similar to our
[commit conventions](#commits).

- `<type>-<description>`
    - Examples include:
        - `feat-add-development-doc`
        - `fix-runtime-error`
    - Types include:
        - `feat`
        - `fix`
        - `refactor`
        - `docs`
        - `test`

### Pull requests

- Open a pull request (PR) for your feature branches
- Use the PR template to add as much information about the feature branch
- Request a review from other code owners when branch is ready for review
