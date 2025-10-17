using Microsoft.Maui.Controls;
using System.Text;

namespace CalculadoraChema
{
    public partial class MainPage : ContentPage
    {
        #region Private Fields
        StringBuilder numActual = new StringBuilder();
        private decimal? solucionArrastrada;
        private decimal memoria = 0;
        private char? signoOperacion;
        private const int MAX_DIGITS = 14;
        #endregion

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnNumeroClicked(object sender, EventArgs e)
        {
            if (numActual.Length < MAX_DIGITS)
            {
                try
                {
                    Button boton = sender as Button;
                    var textoBoton = boton.Text;

                    if (lbPantalla.Text == null || lbPantalla.Text == "0")
                    {
                        numActual.Clear();
                        numActual.Append(textoBoton);
                        ActualizarPantalla();

                    } else
                    {
                        numActual.Append(textoBoton);
                        ActualizarPantalla();
                    }

                } catch (Exception ex)
                {
                    DisplayAlert("Error", "Error", "OK");
                }
            } 
        }

        private void OnOperacionClicked(object sender, EventArgs e)
        {
            try
            {
                Button boton = sender as Button;
                Char textoBoton = boton.Text[0];
                decimal operando;
                /*
                Une todos los elementos en una cadena que luego convierte en un decimal
                Si no se ha introducido ningún número, se opera con la solución arrastrada
                */
                if (numActual.Length == 0)
                {
                    operando = 0;
                } else
                {
                    operando = Decimal.Parse(numActual.ToString());
                }

                /*
                En caso de no haber nada, guarda el número como solución arrastrada
                En caso de haber una operación inconclusa, calcula y luego actualiza la información*/
                if (solucionArrastrada == null)
                {
                    solucionArrastrada = operando;
                    numActual.Clear();
                    ActualizarPantalla();
                } else 
                {
                    switch (textoBoton)
                    {
                        case '+':
                            solucionArrastrada = Operar(operando);
                            numActual.Clear();
                            break;

                        case '-':
                            solucionArrastrada = Operar(operando);
                            numActual.Clear();
                            break;

                        case '*':
                            solucionArrastrada = Operar(operando);
                            numActual.Clear();
                            break;

                        case '/':
                            solucionArrastrada = Operar(operando);
                            numActual.Clear();
                            break;

                        case '=':
                            if (solucionArrastrada != null) MostrarSolucion(Operar(operando));
                            break;
                    }

                }
                
                if (textoBoton != '=')
                {
                    signoOperacion = textoBoton;
                    ActualizarPantalla();
                } else
                {
                    signoOperacion = null;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "Error", "OK");
            }
        }

        private void OnLimpiarClicked(object sender, EventArgs e)
        {
            if (numActual != null) numActual.Clear();
            numActual.Append("0");
            if (solucionArrastrada != null) solucionArrastrada = null;
            if (signoOperacion != null) signoOperacion = null;
            ActualizarPantalla();
        }

        private void OnDecimalClicked(object sender, EventArgs e)
        {
            if (!numActual.ToString().Contains('.'))
            {
                numActual.Append(',');
                ActualizarPantalla();
            }
        }

        private void OnCambiarSignoClicked(object sender, EventArgs e)
        {
            if (numActual.Length == 0) return;

            if (numActual[0] != '-') numActual.Insert(0, "-");
            else numActual.Remove(0, 1);

            ActualizarPantalla();
        }

        private void OnMemoriaClicked(object sender, EventArgs e)
        {
            Button boton = sender as Button;
            var textoBoton = boton.Text;

            switch (textoBoton)
            {
                case "M+":
                    memoria += decimal.Parse(numActual.ToString());
                    ActualizarPantallaMemoria();
                    break;

                case "M-":
                    memoria -= decimal.Parse(numActual.ToString());
                    ActualizarPantallaMemoria();
                    break;

                case "MC":
                    memoria = 0;
                    ActualizarPantallaMemoria();
                    break;

                case "M":
                    numActual.Clear();
                    numActual.Append(memoria);
                    ActualizarPantalla();
                    break;
            }
        }

        private decimal? Operar(decimal operando)
        {
            decimal? solucion = solucionArrastrada;

            switch (signoOperacion)
            {
                case '+':
                    solucion += operando;
                    ActualizarPostOperacion();
                    break;

                case '-':
                    solucion -= operando;
                    ActualizarPostOperacion();
                    break;

                case '*':
                    solucion *= operando;
                    ActualizarPostOperacion();
                    break;

                case '/':
                    solucion /= operando;
                    ActualizarPostOperacion();
                    break;
            }

            return solucion;
        }

        private void ActualizarPostOperacion()
        {
            lbPantalla.Text = $"{solucionArrastrada} {signoOperacion} ";
        }

        private void ActualizarPantalla()
        {
            if (signoOperacion != null)
            {
                lbPantalla.Text = $"{solucionArrastrada} {signoOperacion} {numActual.ToString()}";
            } else
            {
                lbPantalla.Text = numActual.ToString();
            }
        }

        private void ActualizarPantallaMemoria()
        {
            btnPantallaMemoria.Text = $"{memoria}";
        }

        private void MostrarSolucion(decimal? solucion)
        {
            lbPantalla.Text = $"{solucionArrastrada} {signoOperacion} {numActual.ToString()} = {solucion}";
            solucionArrastrada = solucion;
        }
    }
}
