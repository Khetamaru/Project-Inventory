using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_Inventory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<Utilisateur> newList = new List<Utilisateur> {

                new Utilisateur {
                    Nom = "Didier",
                    horizontalAlign = HorizontalAlignment.Left,
                    verticalAlign = VerticalAlignment.Center
                },

                new Utilisateur {
                    Nom = "Patrick",
                    horizontalAlign = HorizontalAlignment.Center,
                    verticalAlign = VerticalAlignment.Center
                }
            };

            List<Button> buttonList = new List<Button>();

            Button temp;
            foreach (Utilisateur utilisateur in newList)
            {
                temp = new Button();
                temp.Content = utilisateur.Nom;
                temp.HorizontalAlignment = utilisateur.horizontalAlign;
                temp.VerticalAlignment = utilisateur.verticalAlign;
                buttonList.Add(temp);
            }

            foreach (Button button in buttonList)
            {
                WindowGrid.Children.Add(button);
            }
        }
    }

    /*
        <ListBox 
            VerticalAlignment="Bottom"
            x:Name="LbxUtilisateurs">
            
            <ListBox.ItemTemplate>
                <DataTemplate>
                    
                    <Grid ShowGridLines="True">
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="300"/>
                            <ColumnDefinition/>
                        </Grid.ColumnDefinitions>
                        
                        <Label Content="{Binding Nom}" />
                        <Label Grid.Column="1" Content="Utilisateur" />
                    </Grid>
                    
                </DataTemplate>
            </ListBox.ItemTemplate>
            
        </ListBox>
    */
}
