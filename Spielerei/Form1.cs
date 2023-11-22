using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spielerei
{
    public partial class Form1 : Form
    {
        //für die Zeichenfläche
        Graphics zeichenflaeche;
        //für die Linienfarbe
        Color linienfarbe;
        //für die Hintergrundfarbe
        Color hintergrundfarbe;

        //für den Linienstil
        System.Drawing.Drawing2D.DashStyle[] linienstil =
        {
            System.Drawing.Drawing2D.DashStyle.Dash,
            System.Drawing.Drawing2D.DashStyle.DashDot,
            System.Drawing.Drawing2D.DashStyle.DashDotDot,
            System.Drawing.Drawing2D.DashStyle.Dot,
            System.Drawing.Drawing2D.DashStyle.Solid
        };

        //für den Hintergrund
        System.Drawing.Drawing2D.HatchStyle[] fuellstil =
        {
            System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal,
            System.Drawing.Drawing2D.HatchStyle.Cross,
            System.Drawing.Drawing2D.HatchStyle.DottedGrid,
            System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal,
            System.Drawing.Drawing2D.HatchStyle.Sphere,
            System.Drawing.Drawing2D.HatchStyle.Vertical,
            System.Drawing.Drawing2D.HatchStyle.Wave,
            System.Drawing.Drawing2D.HatchStyle.ZigZag
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //eine Variable vom Typ Rectangle
            Rectangle bereich;
            //den Client-Bereich beschaffen
            bereich = ClientRectangle;




            //die Linienfarbe ist zunächst schwarz
            linienfarbe = Color.Black;
            //und auch die Hintergrundfarbe
            hintergrundfarbe = Color.Black;
            //für die Lininenstile 
            foreach (System.Drawing.Drawing2D.DashStyle element in linienstil)
                listBoxLinieStil.Items.Add("");
            //für den Hintergrund
            foreach (System.Drawing.Drawing2D.HatchStyle element in fuellstil)
                listBoxHintergrundMuster.Items.Add("");

            //zeichenflaeche ist als Feld der Klasse Form1 vereinbart
            zeichenflaeche = panel1.CreateGraphics();
        }

        private void ButtonLoeschen_Click(object sender, EventArgs e)
        {
            zeichenflaeche.Clear(panel1.BackColor);
        }

        private void ButtonBeenden_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void ButtonStart_Click(object sender, EventArgs e)
        {
            //eine lokale Variable für die Größe
            int groesse = 0;
            //---------------------------------------------//
            //---------------------------------------------//
            //Eine Variable die speichert erste Wert von trackBar Value
            int groesseErsteWert = 0;
            //---------------------------------------------//
            //---------------------------------------------//
            //Eine lokale Variable für Geschwindigkeit
            int geschwindigkeit = 0;
            //---------------------------------------------//
            //---------------------------------------------//
            //einen Stift mit der ausgewählten Farbe erzeugen
            Pen stift = new Pen(linienfarbe);
            //einen Pinsel in der ausgewählten Farbe erzeugen
            SolidBrush pinsel = new SolidBrush(hintergrundfarbe);
            //die Dicke des Stiftes setzen
            stift.Width = Convert.ToInt32(numericUpDownLinieStaerke.Value);
            //die Größe der Figur ermitteln
            switch (trackBar1.Value)
            {
                case 1:
                    groesse = 125;
                    break;
                case 2:
                    groesse = 85;
                    break;
                case 3:
                    groesse = 50;
                    break;
            }
            //den Linienstil ermitteln
            if (listBoxLinieStil.SelectedIndex >= 0)
                stift.DashStyle = linienstil[listBoxLinieStil.SelectedIndex];
            //Figur ermitteln
            //beim Kreis und beim Rechteck auch die Füllung überprüfen
            if (radioButtonKreis.Checked == true)
            {
                if (radioButtonHintergrundOhne.Checked == true)
                {

                    zeichenflaeche.DrawEllipse(stift, panel1.ClientRectangle.Left + groesse, panel1.ClientRectangle.Top + groesse, panel1.ClientRectangle.Width - (groesse * 2), panel1.ClientRectangle.Height - (groesse * 2));
                    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    System.Threading.Thread.Sleep(1000);
                    groesseErsteWert = groesse;

                    geschwindigkeit = (int)(1000 - (Geschwindigkeit.Value * 50));

                    for (int i = 0; i < Wiederholung_Value.Value; i++)
                    {

                        do
                        {

                            groesse -= 10;
                            // zeichenflaeche.DrawEllipse(stift, panel1.ClientRectangle.Left + groesse, panel1.ClientRectangle.Top + groesse, panel1.ClientRectangle.Width - (groesse * 2), panel1.ClientRectangle.Height - (groesse * 2));
                            System.Threading.Thread.Sleep(geschwindigkeit);
                            zeichenflaeche.Clear(BackColor);
                            zeichenflaeche.DrawEllipse(stift, panel1.ClientRectangle.Left + groesse, panel1.ClientRectangle.Top + groesse, panel1.ClientRectangle.Width - (groesse * 2), panel1.ClientRectangle.Height - (groesse * 2));

                            if (panel1.ClientRectangle.Left == panel1.ClientRectangle.Left + groesse)
                                break;


                        } while (panel1.ClientRectangle.Left < panel1.ClientRectangle.Left + groesse);

                        do
                        {
                            groesse += 10;
                            System.Threading.Thread.Sleep(geschwindigkeit);
                            zeichenflaeche.Clear(BackColor);
                            zeichenflaeche.DrawEllipse(stift, panel1.ClientRectangle.Left + groesse, panel1.ClientRectangle.Top + groesse, panel1.ClientRectangle.Width - (groesse * 2), panel1.ClientRectangle.Height - (groesse * 2));

                            if (panel1.ClientRectangle.Left + groesse == panel1.ClientRectangle.Left + groesseErsteWert)
                                break;

                        } while (panel1.ClientRectangle.Left + groesse < panel1.ClientRectangle.Left + groesseErsteWert);

                    }
                    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//                
                }
                if (radioButtonHintergrundFarbe.Checked == true)
                    zeichenflaeche.FillEllipse(pinsel, panel1.ClientRectangle.Left + groesse, panel1.ClientRectangle.Top + groesse, panel1.ClientRectangle.Width - (groesse * 2), panel1.ClientRectangle.Height - (groesse * 2));
                
                //soll mit Muster gezeichnet werden und ist ein Muster ausgewählt?
                if (radioButtonHintergrundMuster.Checked == true && listBoxHintergrundMuster.SelectedIndex >= 0)
                {
                    //einen neuen Pinsel für das Muster erzeugen
                    //die Vordergrundfarbe kommt vom Stift, der Hintergrund ist immer weiß
                    System.Drawing.Drawing2D.HatchBrush musterPinsel = new System.Drawing.Drawing2D.HatchBrush(fuellstil[listBoxHintergrundMuster.SelectedIndex], stift.Color, Color.White);
                    zeichenflaeche.FillEllipse(musterPinsel, panel1.ClientRectangle.Left + groesse, panel1.ClientRectangle.Top + groesse, panel1.ClientRectangle.Width - (groesse * 2), panel1.ClientRectangle.Height - (groesse * 2));
                }
                
            }
            if (radioButtonRechteck.Checked == true)
            {
                if (radioButtonHintergrundOhne.Checked == true)
                {


                    zeichenflaeche.DrawRectangle(stift, panel1.ClientRectangle.Left + groesse, panel1.ClientRectangle.Top + groesse, panel1.ClientRectangle.Width - (groesse * 2), panel1.ClientRectangle.Height - (groesse * 2));
                    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//                
                    groesseErsteWert = groesse;

                    geschwindigkeit = (int)(1000 - (Geschwindigkeit.Value * 50));

                    for (int i = 0; i < Wiederholung_Value.Value; i++)
                    {
                        do
                        {

                            groesse -= 10;
                            System.Threading.Thread.Sleep(geschwindigkeit);
                            zeichenflaeche.Clear(BackColor);
                            zeichenflaeche.DrawRectangle(stift, panel1.ClientRectangle.Left + groesse, panel1.ClientRectangle.Top + groesse, panel1.ClientRectangle.Width - (groesse * 2), panel1.ClientRectangle.Height - (groesse * 2));

                            if (panel1.ClientRectangle.Left == panel1.ClientRectangle.Left + groesse)
                                break;


                        } while (panel1.ClientRectangle.Left < panel1.ClientRectangle.Left + groesse);


                        do
                        {
                            groesse += 10;
                            System.Threading.Thread.Sleep(geschwindigkeit);
                            zeichenflaeche.Clear(BackColor);
                            zeichenflaeche.DrawRectangle(stift, panel1.ClientRectangle.Left + groesse, panel1.ClientRectangle.Top + groesse, panel1.ClientRectangle.Width - (groesse * 2), panel1.ClientRectangle.Height - (groesse * 2));

                            if (panel1.ClientRectangle.Left + groesse == panel1.ClientRectangle.Left + groesseErsteWert)
                                break;

                        } while (panel1.ClientRectangle.Left + groesse < panel1.ClientRectangle.Left + groesseErsteWert);

                    }
                    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//                
                    
                }
                
                if (radioButtonHintergrundFarbe.Checked == true)
                {
                    zeichenflaeche.FillRectangle(pinsel, panel1.ClientRectangle.Left + groesse, panel1.ClientRectangle.Top + groesse, panel1.ClientRectangle.Width - (groesse * 2), panel1.ClientRectangle.Height - (groesse * 2));
                }
            
                //soll mit Muster gezeichnet werden und ist ein Muster ausgewählt?
                if (radioButtonHintergrundMuster.Checked == true && listBoxHintergrundMuster.SelectedIndex >= 0)
                {
                    //einen neuen Pinsel für das Muster erzeugen
                    //die Vordergrundfarbe kommt vom Stift, der Hintergrund ist immer weiß
                    System.Drawing.Drawing2D.HatchBrush musterPinsel = new System.Drawing.Drawing2D.HatchBrush(fuellstil[listBoxHintergrundMuster.SelectedIndex], stift.Color, Color.White);
                    zeichenflaeche.FillRectangle(musterPinsel, panel1.ClientRectangle.Left + groesse, panel1.ClientRectangle.Top + groesse, panel1.ClientRectangle.Width - (groesse * 2), panel1.ClientRectangle.Height - (groesse * 2));
                }
        
            }
            
            if (radioButtonLinie.Checked == true)
            {
                zeichenflaeche.DrawLine(stift, panel1.ClientRectangle.Left + groesse, panel1.ClientRectangle.Height / 2, panel1.ClientRectangle.Width - groesse, panel1.ClientRectangle.Height / 2);
                zeichenflaeche.DrawLine(stift, panel1.ClientRectangle.Width / 2, panel1.ClientRectangle.Top + groesse, panel1.ClientRectangle.Width / 2, panel1.ClientRectangle.Height - groesse);

                //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//                
                groesseErsteWert = groesse;

                geschwindigkeit = (int)(1000 - (Geschwindigkeit.Value * 50));

                for (int i = 0; i < Wiederholung_Value.Value; i++)
                {
                    do
                    {

                        groesse -= 10;
                        System.Threading.Thread.Sleep(geschwindigkeit);
                        zeichenflaeche.Clear(BackColor);
                        zeichenflaeche.DrawLine(stift, panel1.ClientRectangle.Left + groesse, panel1.ClientRectangle.Height / 2, panel1.ClientRectangle.Width - groesse, panel1.ClientRectangle.Height / 2);
                        zeichenflaeche.DrawLine(stift, panel1.ClientRectangle.Width / 2, panel1.ClientRectangle.Top + groesse, panel1.ClientRectangle.Width / 2, panel1.ClientRectangle.Height - groesse);

                        if (panel1.ClientRectangle.Left == panel1.ClientRectangle.Left + groesse)
                            break;


                    } while (panel1.ClientRectangle.Left < panel1.ClientRectangle.Left + groesse);


                    do
                    {
                        groesse += 10;
                        System.Threading.Thread.Sleep(geschwindigkeit);
                        zeichenflaeche.Clear(BackColor);
                        zeichenflaeche.DrawLine(stift, panel1.ClientRectangle.Left + groesse, panel1.ClientRectangle.Height / 2, panel1.ClientRectangle.Width - groesse, panel1.ClientRectangle.Height / 2);
                        zeichenflaeche.DrawLine(stift, panel1.ClientRectangle.Width / 2, panel1.ClientRectangle.Top + groesse, panel1.ClientRectangle.Width / 2, panel1.ClientRectangle.Height - groesse);

                        if (panel1.ClientRectangle.Left + groesse == panel1.ClientRectangle.Left + groesseErsteWert)
                            break;

                    } while (panel1.ClientRectangle.Left + groesse < panel1.ClientRectangle.Left + groesseErsteWert);

                }
                //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//                

            }

        }

        private void ButtonLinienFarbe_Click(object sender, EventArgs e)
        {
            //den Dialog zur Farbauswahl anzeigen
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                //die Hintergrundfarbe für das Panel auf die ausgewählte Farbe setzen
                panelLinienFarbeVorschau.BackColor = colorDialog1.Color;
                //und die Linienfarbe
                //linienfarbe ist ein Feld der Klasse Form1
                linienfarbe = colorDialog1.Color;
            }
        }

        private void ButtonHintergrundFarbe_Click(object sender, EventArgs e)
        {
            //den Dialog zur Farbauswahl anzeigen
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                //die Hintergrundfarbe für das Panel auf die ausgewählte Farbe setzen
                panelHintergrundFarbeVorschau.BackColor = colorDialog1.Color;
                //und die eigentliche Hintergrundfarbe
                //hintergrundfarbe ist ein Feld der Klasse Form1
                hintergrundfarbe = colorDialog1.Color;
                //die Auswahl Farbe aktivieren
                radioButtonHintergrundFarbe.Checked = true;
            }
        }

        private void ListBoxLinieStil_DrawItem(object sender, DrawItemEventArgs e)
        {
            //eine lokale Variable für die Berechnung der Mitte
            int y;
            //ein neuer lokaler Stift
            Pen boxStift = new Pen(Color.Black);
            //die Mitte berechnen
            y = (e.Bounds.Top + e.Bounds.Bottom) / 2;
            //den Hintergrund zeichnen
            e.DrawBackground();
            //und die Linie
            boxStift.DashStyle = linienstil[e.Index];
            e.Graphics.DrawLine(boxStift, e.Bounds.Left + 1, y, e.Bounds.Right - 1, y);
        }

        private void ListBoxHintergrundMuster_DrawItem(object sender, DrawItemEventArgs e)
        {
            //ein neuer lokaler Pinsel für das Muster
            System.Drawing.Drawing2D.HatchBrush boxPinsel = new System.Drawing.Drawing2D.HatchBrush(fuellstil[e.Index], Color.Black, Color.White);
            //den Hintergrund zeichnen
            e.DrawBackground();
            //und das Rechteck
            e.Graphics.FillRectangle(boxPinsel, e.Bounds.Left + 1, e.Bounds.Top + 1, e.Bounds.Width - 1, e.Bounds.Height - 1);
        }
    }
            
}


