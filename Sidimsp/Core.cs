using System;
using System.Collections.Generic;

namespace Sidimsp
{
	public class Core
	{
		public Core (List<int> queueTypes, List<int> quantums)
		{
			_processQueue = new MultiLevelFeedbackQueue( queueTypes, quantums );
			
		}
		
		private MultiLevelFeedbackQueue _processQueue{ get; set; }
		
		// methods
		public void DoWork() {
			
		}
	}
}

