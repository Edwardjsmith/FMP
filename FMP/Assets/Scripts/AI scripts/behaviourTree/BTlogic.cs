
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviourTree
{
    public class Task 
    {
        public enum taskState
        {
            NotSet,
            Success,
            Failure,
            Running
        };

        protected taskState state;
        public List<Task> childTasks;
        protected baseAI bot;

        public string name;
        public Vector2 uiPos;
        public Button asignedNode;


        public Task(baseAI bot, Vector3 pos, bool trueOrFalse, List<Task> tasks, string name, Vector2 uiPos)
        {
            this.uiPos = uiPos;
            this.name = name;
            tasks.Add(this);
            this.bot = bot;
            childTasks = new List<Task>();
        }

        public Task(baseAI bot, List<Task> tasks, string name, Vector2 uiPos)
        {
            this.uiPos = uiPos;
            this.name = name;
            tasks.Add(this);
            this.bot = bot;
            childTasks = new List<Task>();
        }

        public Task(baseAI bot, Vector3 pos, bool trueOrFalse, List<Task> tasks, string name)
        {
            this.name = name;
            tasks.Add(this);
            this.bot = bot;
            childTasks = new List<Task>();
        }

        public Task(baseAI bot, List<Task> tasks, string name)
        {
            this.name = name;
            tasks.Add(this);
            this.bot = bot;
            childTasks = new List<Task>();
        }


        public virtual void Start()
        {
            state = taskState.Running;
        }
        protected void Succeed()
        {
            state = taskState.Success;
        }

        protected void Fail()
        {
            state = taskState.Failure;
        }

        protected void Run()
        {
            state = taskState.Running;
        }

        public bool isSuccess()
        {
            return state.Equals(taskState.Success);
        }

        public bool isFailure()
        {
            return state.Equals(taskState.Failure);
        }

        public bool isRunning()
        {
            return state.Equals(taskState.Running);
        }

        public taskState currentState()
        {
            return state;
        }

        public void addChild(int index, Task task)
        {
            childTasks.Insert(index, task);
        }

        public void calculateChildUIPos()
        {
            for (int i = 0; i < childTasks.Count; i++)
            {
                float offsetX = -50;
                float offsetY = -50;

                for (int j = 0; j < i; j++)
                {
                    offsetX += 100;
                }
                    
                childTasks[i].uiPos = new Vector2(uiPos.x + offsetX, uiPos.y + offsetY);

                if (childTasks[i].name == "Check ray")
                {
                    childTasks[i].uiPos = new Vector2(childTasks[i].uiPos.x, childTasks[i].uiPos.y - 50);
                }
            }
        }

        public virtual taskState evaluateTask()
        {
            return state;
        }

        public virtual void reset()
        {
            state = taskState.NotSet;
        }
    }


    class Selector : Task
    {
        public Selector(baseAI bot, string name, List<Task> tasks, Vector2 uiPos) : base(bot, tasks, name, uiPos)
        {
            
        }
        public Selector(baseAI bot, string name, List<Task> tasks) : base(bot, tasks, name)
        {

        }

        public override taskState evaluateTask()
        {
            //while (state == taskState.Running)
            {
                reset();
                foreach (Task t in childTasks)
                {
                    t.reset();
                    t.evaluateTask();
                    switch (t.currentState())
                    {
                        case taskState.Failure:
                            continue;
                        case taskState.Success:
                            state = taskState.Success;
                            return state;
                        case taskState.Running:
                            state = taskState.Running;
                            return state;
                        default:
                            continue;    
                    }
                }
                state = taskState.Failure;
                return state;
            }
            
        }
    }

    public class Sequence : Task
    {
        bool childRunning = false;
        public Sequence(baseAI bot, string name, List<Task> tasks, Vector2 uiPos) : base(bot, tasks, name, uiPos)
        {
        }

        public Sequence(baseAI bot, string name, List<Task> tasks) : base(bot, tasks, name)
        {
        }

        public override taskState evaluateTask()
        {
            //while(state != taskState.Failure)
            {
                reset();
                foreach (Task t in childTasks)
                {
                    t.reset();
                    t.evaluateTask();
                    switch (t.currentState())
                    {
                        case taskState.Failure:
                            Fail();
                            return state;

                        case taskState.Success:
                            continue;

                        case taskState.Running:
                            childRunning = true;
                            continue;

                        default:
                            Succeed();
                            return state;

                    }
                }

                state = childRunning ? taskState.Running : taskState.Success;
                return state;
            }
        }
    }
}

            
        
    

    


