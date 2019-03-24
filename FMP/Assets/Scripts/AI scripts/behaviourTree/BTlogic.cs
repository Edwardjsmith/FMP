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
        protected agentActions botActions;

        public Task(agentActions bot, Vector3 pos, bool trueOrFalse)
        {
            botActions = bot;
            childTasks = new List<Task>();
        }

        public Task(agentActions bot)
        {
            botActions = bot;
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

        public void addChild(int index, Task task, Vector3 pos)
        {
            childTasks.Insert(index, task);
        }

        public void addChild(int index, Task task, bool trueOrFalse)
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
        public Selector(agentActions bot) : base(bot, Vector3.zero, false)
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
            }
            Fail();
        }
    }

    public class Repeat : Task
    {
        public Repeat(agentActions bot) : base(bot)
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
        public Sequence(agentActions bot) : base(bot, Vector3.zero, false)
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
                }
            }
            Succeed();
        }
    }

}
