using System.Collections.Generic;
using UnityEngine;

namespace Tests.Mock
{

public class MockVocabulary : Vocabulary
{
    public override Dictionary<string, string> GetVocabularyOnly()
    {
        // Return a mock dictionary
        return new Dictionary<string, string>
        {
            {"FrenchWord1", "EnglishWord1"},
            {"FrenchWord2", "EnglishWord2"},
        };
    }
}
}