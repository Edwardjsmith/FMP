
using System.Collections.Generic;

public interface IGOAP
{
    HashSet<KeyValuePair<string, bool>> worldState();
    HashSet<KeyValuePair<string, bool>> createGoal();

    //void planFailed(HashSet<KeyValuePair<string, bool>> failedGoal);
    //void planFound(HashSet<KeyValuePair<string, bool>> goal, Queue<goapAction> actions);
    //void actionsFinished();
    //void planAborted(goapAction abortedAction);
    bool moveTo(goapAction nextAction);
}
