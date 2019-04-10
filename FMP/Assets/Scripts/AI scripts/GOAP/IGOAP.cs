
using System.Collections.Generic;

public interface IGOAP
{
    HashSet<KeyValuePair<string, bool>> worldState();
    HashSet<KeyValuePair<string, bool>> createGoal();

}
