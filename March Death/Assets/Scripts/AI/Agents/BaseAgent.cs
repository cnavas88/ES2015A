﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.Agents
{
    public abstract class BaseAgent
    {
		public const int MAX_AGENT_CONFIDENCE = 1000;
        /// <summary>
        /// This confidence will be applied to every confidence return by this agent.
        /// </summary>
        public int baseConfidence { get; set; }
		public int extraConfidence {get; set; }
        protected AIController ai;
        public float modifier { get; set; }
        public string agentName;
        public BaseAgent(AIController ai, String name)
        {
            this.ai = ai;
            baseConfidence = 0;
            modifier = 1;
            agentName = name;
        }
        public abstract int getConfidence(Squad squad);
        public abstract void controlUnits(Squad squad);

        public virtual void PreUpdate() { }
        public virtual void PostSquad() { }
        public virtual void PostUpdate() { }

		/// <summary>
		/// Transfers confidence to the agent.
		/// </summary>
		/// <param name="confidenceToAdd">Confidence to add.</param>
		public void addConfidence(int confidenceToAdd)
		{
			if(baseConfidence + confidenceToAdd > MAX_AGENT_CONFIDENCE)
			{
				extraConfidence = MAX_AGENT_CONFIDENCE;
			}
			else
			{
				extraConfidence += confidenceToAdd;
			}

		}

		/// <summary>
		/// Uses the extra confidence.
		/// </summary>
		/// <returns>The extra confidence.</returns>
		public int useExtraConfidence(){
			int c;
			c = extraConfidence;
			extraConfidence = 0;
			return c;
		}
    }
}
