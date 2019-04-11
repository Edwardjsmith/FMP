
using System.Collections.Generic;

public interface IGOAP
{
    HashSet<KeyValuePair<string, bool>> setWorldState();
    HashSet<KeyValuePair<string, bool>> createGoal();

}
