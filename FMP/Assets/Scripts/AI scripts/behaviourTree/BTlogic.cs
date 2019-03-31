using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

        public Task(baseAI bot, Vector3 pos, bool trueOrFalse)
        {
            this.bot = bot;
            childTasks = new List<Task>();
        }

        public Task(baseAI bot)
        {
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

        public virtual taskState evaluateTask()
        {
            return state;
        }

        public virtual void reset()
        {
            Run();
        }
    }


    class Selector : Task
    {
        public Selector(baseAI bot) : base(bot)
        {

        }

        public override taskState evaluateTask()
        {
            foreach (Task t in childTasks)
            {
                t.evaluateTask();
                switch (t.currentState())
                {
                    case taskState.Failure:
                        continue;
                    case taskState.Success:
                        return state = taskState.Success;
                    case taskState.Running:
                        return state = taskState.Running;
                    default:
                        continue;
                }
            }
            return state = taskState.Failure;
        }
    }

    public class Sequence : Task
    {
        bool childRunning = false;
        public Sequence(baseAI bot) : base(bot)
        {

        }

        public override taskState evaluateTask()
        {
            
            foreach (Task t in childTasks)
            {
                t.evaluateTask();
                switch (t.currentState())
                {
                    case taskState.Failure:
                        Fail();
                        return currentState();

                    case taskState.Success:
                        continue;

                    case taskState.Running:
                        childRunning = true;
                        continue;

                    default:
                        Succeed();
                        return currentState();
                }
            }

            return state = childRunning ? taskState.Running : taskState.Success;
        }
    }
}

            
        
    

    


