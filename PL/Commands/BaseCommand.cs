using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.Commands;

internal abstract class BaseCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    /// <summary>
    /// function that return if the boutton is clicable
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public virtual bool CanExecute(object? parameter)
    {
        return true;
    }

    /// <summary>
    /// Function is preformd when the boutton is cliced
    /// </summary>
    /// <param name="parameter"></param>
    public abstract void Execute(object? parameter);

    /// <summary>
    /// Function when we want to change the can executed status
    /// </summary>
    protected void OnCanExecutedChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
