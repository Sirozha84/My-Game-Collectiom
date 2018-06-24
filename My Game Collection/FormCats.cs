﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace My_Game_Collection
{
    public partial class FormCats : Form
    {
        object list;
        int type; //Тип каталога... сделаю пока по ламерски, так как не разобрался как правильно :-)

        public FormCats(int type, object list)
        {
            InitializeComponent();
            this.list = list;
            this.type = type;
            if (type == 1) Text = "Справочник \"Платформы\"";
            if (type == 2) Text = "Справочник \"Носители / электронные магазины\"";
            DrawList();
        }

        void DrawList()
        {
            
            listViewCat.Items.Clear();
            listViewCat.BeginUpdate();
            if (type == 1)
            {
                ((List<Platform>)list).Sort();
                foreach (Platform item in (List<Platform>)list)
                    listViewCat.Items.Add(item.GetListViewItem());
            }
            listViewCat.EndUpdate();
            listViewCat_SelectedIndexChanged(null, null);
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            if (type == 1)
            {
                Platform item = new Platform();
                FormPlatform form = new FormPlatform(item);
                if (form.ShowDialog() == DialogResult.OK) ((List<Platform>)list).Add(item);
            }
            DrawList();
        }

        private void listViewCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEdit.Enabled = listViewCat.SelectedItems.Count > 0;
            buttonDel.Enabled = listViewCat.SelectedItems.Count > 0;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewCat.SelectedItems.Count == 0) return;
            if (type == 1)
            {
                FormPlatform form = new FormPlatform((Platform)listViewCat.SelectedItems[0].Tag);
                form.ShowDialog();
            }
            DrawList();
        }

        private void listViewCat_DoubleClick(object sender, EventArgs e)
        {
            buttonEdit_Click(null, null);
        }

        private void listViewCat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) buttonEdit_Click(null, null);
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (listViewCat.SelectedItems.Count == 0) return;
            if (MessageBox.Show("Вы уверены что хотите удалить элемент справочника?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (type == 1) ((List<Platform>)list).Remove((Platform)listViewCat.SelectedItems[0].Tag);
            }
            DrawList();
        }
    }
}
