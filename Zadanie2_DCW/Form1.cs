using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Zadanie2_DCW
{
    public partial class Form1 : Form
    {
        //klasa do zserializowania
        [Serializable] 
        public class Client
        {
            public string name;
            public string surname;
            public int age;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void serializeButton_Click(object sender, EventArgs e)
        {
            //obiekt do serializacji
            Client client = new Client();

            //okno zapisu do pliku
            saveFileDialog1.Filter = "Pliki binarne (*.dat)|*.dat";
            saveFileDialog1.Title = "Zapisz do pliku";
            
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fileStream = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                try
                {
                    //pobranie danych z pól
                    client.name = tbName.Text;
                    client.surname = tbSurname.Text;
                    client.age = (int)numericAge.Value;

                    //serializacja
                    binaryFormatter.Serialize(fileStream, client);

                    MessageBox.Show("Obiekt został zserializowany", "Zapisano");
                }
                catch (SerializationException ex)
                {
                    MessageBox.Show("Wystąpił błąd przy serializacji\n" + ex.Message, "Błąd");
                }
                finally
                {
                    fileStream.Close();
                }
            }
        }

        private void deserializeButton_Click(object sender, EventArgs e)
        {
            //obiekt do zapisu po deserializacji
            Client client = new Client();

            //okno wyboru pliku do wczytania
            openFileDialog1.Filter = "Pliki binarne (*.dat)|*.dat";
            openFileDialog1.Title = "Otworz plik";

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fileStream = new FileStream(openFileDialog1.FileName, FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                try
                {
                    //deserializacja
                    client = (Client)binaryFormatter.Deserialize(fileStream);

                    //uzupełnienie pól danymi z deserializacji
                    tbName.Text = client.name;
                    tbSurname.Text = client.surname;
                    numericAge.Value = client.age;

                    MessageBox.Show("Obiekt został zdeserializowany", "Wczytano");
                }
                catch (SerializationException ex)
                {
                    MessageBox.Show("Wystąpił błąd przy deserializacji\n" + ex.Message, "Błąd");
                }
                finally
                {
                    fileStream.Close();
                }
            }
        }
    }
}
