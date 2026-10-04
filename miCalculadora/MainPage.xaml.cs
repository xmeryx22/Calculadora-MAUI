namespace miCalculadora;

public partial class MainPage : ContentPage
{
    // Variables para calcular y memoria
    private double subtotal = 0;
    private double memoria = 0;
    private string operadorPendiente = string.Empty;
    private bool esNuevoNumero = true;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnNumeroClicked(object sender, EventArgs e)
    {
        Button boton = (Button)sender;
        string digito = boton.Text;

        if (esNuevoNumero || labelResultado.Text == "0")
        {
            labelResultado.Text = digito;
            esNuevoNumero = false;
        }
        else
        {
            labelResultado.Text += digito;
        }

    }

    private void OnComaClicked(object sender, EventArgs e)
    {

        if (esNuevoNumero)
        {
            labelResultado.Text = "0,";
            esNuevoNumero = false;
        }
        else if (!labelResultado.Text.Contains(","))
        {
            labelResultado.Text += ",";
        }

    }

    private void OnOperadorClicked(object sender, EventArgs e)
    {
        Button boton = sender as Button;
        string nuevoOperador = boton.Text;

        double valorActual = Convert.ToDouble(labelResultado.Text.Replace(".", ","));

        if (!string.IsNullOrEmpty(operadorPendiente) && !esNuevoNumero)
        {
            subtotal = RealizarCalculo(subtotal, valorActual, operadorPendiente);
            labelResultado.Text = subtotal.ToString();
        }
        else
        {
            subtotal = valorActual;
        }

        operadorPendiente = nuevoOperador;
        esNuevoNumero = true;

    }

    private void OnIgualClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(operadorPendiente))
        {
            double valorActual = Convert.ToDouble(labelResultado.Text.Replace(".", ","));
            subtotal = RealizarCalculo(subtotal, valorActual, operadorPendiente);
            labelResultado.Text = subtotal.ToString();
            operadorPendiente = string.Empty;
            esNuevoNumero = true;
        }
    }

    private double RealizarCalculo(double num1, double num2, string operador)
    {
        return operador switch
        {
            "+" => num1 + num2,
            "-" => num1 - num2,
            "*" => num1 * num2,
            "/" => num2 != 0 ? num1 / num2 : 0,
            _ => num2
        };
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        labelResultado.Text = "0";
        subtotal = 0;
        operadorPendiente = string.Empty;
        esNuevoNumero = true;
    }

    private void OnMemoriaClicked(object sender, EventArgs e)
    {
        Button boton = (Button)sender;
        string accion = boton.Text;
        double valorResultado = Convert.ToDouble(labelResultado.Text.Replace(".", ","));

        switch (accion)
        {

            case "MC": // borra la memoria
                memoria = 0;
                break;

            case "M+": // suma el resultado a la memoria
                memoria += valorResultado;
                break;

            case "M-": // resta el resultado a la memoria
                memoria -= valorResultado;
                break;

            case "M":
                labelResultado.Text = memoria.ToString();
                esNuevoNumero = true;
                break;
        }

        labelMemoria.Text = $"Mem: {memoria}";

    }

    /*private void OnCounterClicked(object? sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }*/
}
