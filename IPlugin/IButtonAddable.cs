using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace IPlugin
{
    public interface IButtonAddable
    {
        RadioButton GenerateButtons(Type type, UniformGrid grid);
    }
}
