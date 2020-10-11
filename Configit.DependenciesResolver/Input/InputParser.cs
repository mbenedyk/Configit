using System;
using System.Linq;

namespace Configit.DependenciesResolver.Input
{
    internal class InputParser
    {
        private const string InvalidInputStructure = "Invalid input structure";

        public ParseResult Parse(string input)
        {
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var result = new ParseResult();

            try
            {
                using (var enumerator = lines.GetEnumerator())
                {
                    if (!enumerator.MoveNext())
                    {
                        throw new InvalidInputException(InvalidInputStructure);
                    }

                    result.NumberOrPackages = int.Parse(enumerator.Current);

                    for (int i = 0; i < result.NumberOrPackages; i++)
                    {
                        if (!enumerator.MoveNext())
                        {
                            throw new InvalidInputException(InvalidInputStructure);
                        }

                        result.Packages.Add(enumerator.Current);
                    }

                    if (!enumerator.MoveNext())
                    {
                        return result;
                    }

                    result.NumberOfDependencies = int.Parse(enumerator.Current);
                    for (int i = 0; i < result.NumberOfDependencies; i++)
                    {
                        if (!enumerator.MoveNext())
                        {
                            throw new InvalidInputException(InvalidInputStructure);
                        }

                        result.Dependencies.Add(enumerator.Current);
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                if (e is InvalidInputException operationException)
                {
                    throw operationException;
                }

                throw new InvalidInputException("Unhandled exception", e);
            }
        }
    }
}