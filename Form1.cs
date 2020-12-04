using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace BiblWorm
{
    public partial class Form1 : Form
    {
        public string Author // автор
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        public string Title // Название
        {
            get { return textBox2.Text; }
            set { textBox3.Text = value; }
        }

        public string PublishHouse // Издательство
        {
            get { return textBox3.Text; }
            set { textBox2.Text = value; }
        }

        public int Page // Количество страниц
        {
            get { return (int)numericUpDown1.Value; }
            set { numericUpDown1.Value = value; }
        }
        public int Year // Год издания
        {
            get { return (int)numericUpDown2.Value; }
            set { numericUpDown2.Value = value; }
        }
        public int InvNumber // Инвентарный номер
        {
            get { return (int)numericUpDown3.Value; }
            set { numericUpDown3.Value = value; }
        }
        public bool Existence // Наличие
        {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }
        public bool SortInvNumber // Сортировка по инвентарному номеру
        {
            get { return checkBox2.Checked; }
            set { checkBox2.Checked = value; }
        }
        public bool ReturnTime // Возвращение в срок
        {
            get { return checkBox3.Checked; }
            set { checkBox3.Checked = value; }
        }
        public int PeriodUse // Инвентарный номер
        {
            get { return (int)numericUpDown4.Value; }
            set { numericUpDown4.Value = value; }
        }
        // List<ClBook> its = new List<ClBook>();
        ArrayList its = new ArrayList();
        ArrayList zhur = new ArrayList();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClBook b = new ClBook(Author, Title, PublishHouse,
            Page, Year, InvNumber, Existence);
            if (ReturnTime)
                b.ReturnSrok();
            b.PriceBook(PeriodUse);
            its.Add(b);
            Author = Title = PublishHouse = "";
            Page = InvNumber = PeriodUse = 1;
            Year = 1;
            Existence = ReturnTime = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ClBook b = new ClBook(textBox1.Text, textBox3.Text, textBox2.Text, (int)numericUpDown4.Value, (int)numericUpDown3.Value, (int)numericUpDown2.Value, checkBox1.Checked);
            if (ReturnTime)
                b.ReturnSrok();
            b.PriceBook(PeriodUse);
            its.Add(b);
            Author = Title = PublishHouse = "";
            Page = InvNumber = PeriodUse = 1;
            Year = 1;
            Existence = ReturnTime = false;

            listBox1.Items.Add(b.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SortInvNumber)
                its.Sort();
            StringBuilder sb = new StringBuilder();
            foreach (Item item in its)
            {
                sb.Append("\n" + item.ToString());            
                listBox1.Items.Add(item.ToString());
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ClBook b = new ClBook(textBox6.Text, textBox5.Text, textBox4.Text, (int)numericUpDown8.Value, (int)numericUpDown7.Value, (int)numericUpDown6.Value, checkBox5.Checked);
            if (ReturnTime)
                b.ReturnSrok();
            b.PriceBook(PeriodUse);
            zhur.Add(b);
            Author = Title = PublishHouse = "";
            Page = InvNumber = PeriodUse = 1;
            Year = 1;
            Existence = ReturnTime = false;

            listBox2.Items.Add(b.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (SortInvNumber)
                zhur.Sort();
            StringBuilder sb = new StringBuilder();
            foreach (Item item in zhur)
            {
                sb.Append("\n" + item.ToString());
                listBox2.Items.Add(item.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }


        abstract public class Item : IComparable
        {
            protected long invNumber; // инвентарный номер — целое число
            protected bool taken;    // хранит состояние объекта - взят ли на руки

            //  abstract public void Return();    // операция "вернуть"
            public long InvNumber
            {
                get { return invNumber; }
                set { invNumber = value; }
            }
            public bool Taken
            {
                get { return taken; }
                set { taken = value; }
            }
            public Item(long invNumber, bool taken)
            {
                this.invNumber = invNumber;
                this.taken = taken;
            }

            public Item()
            {
                this.taken = true;
            }

            public bool IsAvailable()    // истина, если этот предмет имеется в библиотеке
            {
                if (taken == true)
                    return true;
                else
                    return false;
            }
            public long GetInvNumber()   // инвентарный номер  
            {
                return invNumber;
            }
            private void Take()      // операция "взять"
            {
                taken = false;
            }

            public void TakeItem()
            {
                if (this.IsAvailable())
                    this.Take();
            }
            public override string ToString()
            {
                if (taken)
                    return "Состояние хранения: Инвентарный номер: " + invNumber + ". В наличии";
                else
                    return "Состояние хранения: Инвентарный номер: " + invNumber + ". Нет в наличии";
            }
            int IComparable.CompareTo(object obj)
            {
                Item it = (Item)obj;
                if (this.invNumber == it.invNumber) return 0;
                else if (this.invNumber > it.invNumber) return 1;
                else return -1;
            }
        }

        class ClBook : Item
        {
            private String author;
            private String title;
            private String publisher;
            private int pages;
            private int year;

            private double cust;
            private bool returnSrok;

            private static double price = 9;
            //  private bool taken;

            static ClBook()       //статический конструктор
            {
                price = 100;
            }


            public ClBook(String author, String title, String publisher, int pages, int year, long invNumber, bool taken) : base(invNumber, taken)
            {
                this.author = author;
                this.title = title;
                this.publisher = publisher;
                this.pages = pages;
                this.year = year;
            }

            public ClBook()
            { }
            public static void SetPrice(double price)
            {
                ClBook.price = price;
            }

            public override string ToString()
            {
                if (this.IsAvailable())
                    return "\nКнига:\n Автор: " + author + "\n Название: " + title +
                    "\n Год издания: " + year + "., " + pages + " стр. \n Стоимость аренды: " + ClBook.price + " p.\n" + base.ToString()
                    + "\nИтого за чтение: " + cust + " p.";
                else
                    return "\nКнига:\n Автор: " + author + "\n Название: " + title +
                "\n Год издания: " + year + "., " + pages + " стр. \n Стоимость аренды: " + ClBook.price + " p.\n" + base.ToString();

            }
            public void PriceBook(int s)
            {

                if (this.returnSrok == true)
                    this.cust = s * price;
                else this.cust = s * (price + price * 0.13); ;

            }

            public void ReturnSrok()
            {
                returnSrok = true;
            }

            public void Return()    // операция "вернуть"
            {
                if (returnSrok == true)
                    taken = true;
                else
                    taken = false;
            }

        }
        class Magazine : Item, IPubs
        {
            private String volume;    // том
            private int number;        // номер
            private String title;       // название
            private int year;      // дата выпуска

            public bool IfSubs { get; set; } // подписка на журнал

            public Magazine(String volume, int number, String title, int year, long invNumber, bool taken)
                : base(invNumber, taken)
            {
                this.volume = volume;
                this.number = number;
                this.title = title;
                this.year = year;
                this.InvNumber = invNumber;
                this.Taken = taken;
            }
            //   public void InvNumber()    // операция "вернуть"
            // {
            //       
            //  }

            // реализация интерфейса
            public void Subs()
            {
                // действия при оформлении подписки на журнал
            }


            public override string ToString()
            {
                if (IfSubs)
                    return "\nЖурнал:\n Название: " + title + "\nТом: " + volume +
                    "\n Номер: " + number + "\nГод выпуска: " + year + "\n Подписка оформлена";
                else
                    return "\nЖурнал:\n Название: " + title + "\nТом: " + volume +
                "\n Номер: " + number + "\nГод выпуска: " + year + "\n Подписка не оформлена"; ;
            }
        }
        interface IPubs
        {
            void Subs();
        }       
    }
}

