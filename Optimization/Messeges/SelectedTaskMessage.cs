using CommunityToolkit.Mvvm.Messaging.Messages;
using Optimization.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization.Messeges
{
    public class SelectedTaskMessage : ValueChangedMessage<ITask>
    {
        public SelectedTaskMessage(ITask value) : base(value)
        {
        }
    }
}
