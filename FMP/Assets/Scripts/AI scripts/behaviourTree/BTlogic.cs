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

        public taskState returnState()
        {
            return state;
        }

        public void addChild(int index, Task task)
        {
            childTasks.Insert(index, task);
        }

        public virtual void runTask()
        {
           
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

        public override void runTask()
        {
            foreach(Task t in childTasks)
            {
                t.Start();
                t.runTask();
                if(t.isRunning())
                {
                    Succeed();
                }
                else
                {
                    continue;
                }
            }
            Fail();
        }
    }

    public class Repeat : Task
    {
        public Repeat(baseAI bot) : base(bot)
        {
          
        }

        public override void runTask()
        {
            foreach (Task t in childTasks)
            {
                t.Start();
                t.runTask();
            }
        }
    }

    public class Sequence : Task
    {
        public Sequence(baseAI bot) : base(bot)
        {

        }

        public override void runTask()
        {
            foreach (Task t in childTasks)
            {
                t.Start();
                t.runTask();
                if(t.isFailure())
                {
                    Fail();
                    break;
                }
            }
            if (isFailure())
            {
                Succeed();
            }
        }
    }

}
