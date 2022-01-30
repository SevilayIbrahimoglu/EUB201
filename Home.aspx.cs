using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class Home : System.Web.UI.Page
    {

        SqlConnection connection;

        public List<Note> notes = new List<Note>();

        static Note editNote = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            closeButton.Visible = false;

            connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["notesDb"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from noteList", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Note note = new Note();
                note.ID = reader.GetInt32(0);
                Console.WriteLine("ID = " + note.ID);
                note.noteTitle = reader.GetString(1);
                note.note = reader.GetString(2);
                note.createDate = reader.GetString(3);
                note.lastUpdateDate = reader.GetString(4);

                notes.Add(note);
            }
            connection.Close();
            myTable.HorizontalAlign = HorizontalAlign.Center;
            inputTable.HorizontalAlign = HorizontalAlign.Center;

            if (notes.Count > 0)
            {
                TableHeaderRow headerRow = new TableHeaderRow();
                TableHeaderCell editHeaderCell = new TableHeaderCell();
                editHeaderCell.Text = "Düzenle";
                TableHeaderCell deleteHeaderCell = new TableHeaderCell();
                deleteHeaderCell.Text = "Sil";
                TableHeaderCell noteHeaderCell = new TableHeaderCell();
                noteHeaderCell.Text = "Not";
                TableHeaderCell createHeaderCell = new TableHeaderCell();
                createHeaderCell.Text = "Oluşturulma Tarihi";
                TableHeaderCell updateHeaderCell = new TableHeaderCell();
                updateHeaderCell.Text = "Son Düzenleme Tarihi";
                headerRow.Cells.Add(editHeaderCell);
                headerRow.Cells.Add(deleteHeaderCell);
                headerRow.Cells.Add(noteHeaderCell);
                headerRow.Cells.Add(createHeaderCell);
                headerRow.Cells.Add(updateHeaderCell);
                myTable.Rows.Add(headerRow);
            } else
            {
                TableHeaderRow headerRow = new TableHeaderRow();
                TableHeaderCell emptyHeader = new TableHeaderCell();
                emptyHeader.Text = "Kayıtlı not bulunmamaktadır.";
                headerRow.Cells.Add(emptyHeader);
                myTable.Rows.Add(headerRow);
            }


            foreach (Note note in notes)
            {
                TableRow trow = new TableRow();
                ImageButton editButton = new ImageButton();
                editButton.ImageUrl = "Images/edit.png";
                editButton.Width = 30;
                editButton.Height = 30;
                editButton.CommandArgument = note.ID.ToString();
                editButton.Click += editNoteClicked;

                TableCell editCell = new TableCell();
                editCell.Controls.Add(editButton);
                editCell.HorizontalAlign = HorizontalAlign.Center;
                editCell.Width = new Unit(10, UnitType.Percentage);
                trow.Cells.Add(editCell);


                TableCell deletecell = new TableCell();
                ImageButton deleteButton = new ImageButton();
                deleteButton.ImageUrl = "Images/garbage.png";
                deleteButton.Width = 30;
                deleteButton.Height = 30;
                deleteButton.CommandArgument = note.ID.ToString();
                deleteButton.Click += deleteNote;
                deletecell.Controls.Add(deleteButton);
                deletecell.Width = new Unit(10, UnitType.Percentage);
                deletecell.HorizontalAlign = HorizontalAlign.Center;
                trow.Cells.Add(deletecell);

                TableCell textCell = new TableCell();
                String noteText = note.note;
                if (noteText.Length > 50)
                {
                    noteText = noteText.Substring(0, 50) + "...";
                }
                textCell.Text = String.Format("<b>" + note.noteTitle + "</b><br />" + noteText);
                textCell.HorizontalAlign = HorizontalAlign.Left;
                textCell.Width = new Unit(40, UnitType.Percentage);
                trow.Cells.Add(textCell);

                TableCell createDate = new TableCell();
                createDate.Text = note.createDate;
                createDate.HorizontalAlign = HorizontalAlign.Center;
                createDate.Width = new Unit(20, UnitType.Percentage);
                trow.Cells.Add(createDate);

                TableCell lastUpdateCel = new TableCell();
                lastUpdateCel.Text = note.lastUpdateDate;
                lastUpdateCel.HorizontalAlign = HorizontalAlign.Center;
                lastUpdateCel.Width = new Unit(20, UnitType.Percentage);
                trow.Cells.Add(lastUpdateCel);
                myTable.Rows.Add(trow);
            }
           
        }

        protected void deleteNote(object sender, ImageClickEventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            int id = Int32.Parse(imageButton.CommandArgument);
            Console.WriteLine("AAAA id " + id);
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM noteList WHERE noteId = " + id, connection);
            command.ExecuteNonQuery();
            connection.Close();
            Response.Redirect(Request.RawUrl);
        }

        protected void editNoteClicked(object sender, ImageClickEventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = false;
            Page.SetFocus(bodyTextBox);
            ImageButton imageButton = (ImageButton)sender;
            int id = Int32.Parse(imageButton.CommandArgument);
            int i = 0;
            while (i < notes.Count && notes[i].ID != id)
            {
                i++;
            }

            if (i < notes.Count)
            {
                NewNoteTitleLabel.Text = "Güncelle";
                editNote = notes[i];
                titleTextBox.Text = editNote.noteTitle;
                bodyTextBox.Text = editNote.note;
                closeButton.Visible = true;
                saveButton.Text = "Güncelle";
            }
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (!validate())
            {
                // TODO add warning about empty inputs
                return;
            }
            if (editNote == null)
            {
                saveNewNote();
            } else
            {
                editTheNote();
            }
        }

        protected void addNewNoteClicked(object sender, EventArgs e)
        {
            cleanInputs();
            Page.MaintainScrollPositionOnPostBack = false;
            Page.SetFocus(bodyTextBox);
        }

        protected void closeButton_Click(object sender, ImageClickEventArgs e)
        {
            cleanInputs();
        }

        private void saveNewNote()
        {
            String createTime = DateTime.Now.ToString();
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO noteList (note,createTime,noteTitle, lastUpdateTime) " +
                "VALUES (@note, @createTime, @noteTitle, @lastUpdateTime)", connection);
            SqlParameter noteParam = new SqlParameter("@note", SqlDbType.VarChar, 8000);
            SqlParameter createTimeParam = new SqlParameter("@createTime", SqlDbType.VarChar, 30);
            SqlParameter noteTitleParam = new SqlParameter("@noteTitle", SqlDbType.VarChar, 50);
            SqlParameter lastUpdateTimeParam = new SqlParameter("@lastUpdateTime", SqlDbType.VarChar, 30);
            noteParam.Value = bodyTextBox.Text;
            createTimeParam.Value = createTime;
            noteTitleParam.Value = titleTextBox.Text;
            lastUpdateTimeParam.Value = "";
            command.Parameters.Add(noteParam);
            command.Parameters.Add(createTimeParam);
            command.Parameters.Add(noteTitleParam);
            command.Parameters.Add(lastUpdateTimeParam);
            command.Prepare();
            command.ExecuteNonQuery();
            connection.Dispose();
            connection.Close();
            Response.Redirect(Request.RawUrl);
        }

        private void editTheNote()
        {
            String editTime = DateTime.Now.ToString();
            connection.Open();
            SqlCommand command = new SqlCommand("UPDATE noteList SET " +
                "noteTitle = @noteTitle , " +
                "note = @note , "+
                "lastUpdateTime = @lastEditTime " +
                "WHERE noteId = @noteId"
                , connection);
            SqlParameter noteParam = new SqlParameter("@note", SqlDbType.VarChar, 8000);
            SqlParameter idParam = new SqlParameter("@noteId", SqlDbType.Int, 0);
            SqlParameter noteTitleParam = new SqlParameter("@noteTitle", SqlDbType.VarChar, 50);
            SqlParameter lastUpdateTimeParam = new SqlParameter("@lastEditTime", SqlDbType.VarChar, 30);
            noteParam.Value = bodyTextBox.Text;
            noteTitleParam.Value = titleTextBox.Text;
            lastUpdateTimeParam.Value = editTime;
            idParam.Value = editNote.ID;
            command.Parameters.Add(noteParam);
            command.Parameters.Add(idParam);
            command.Parameters.Add(noteTitleParam);
            command.Parameters.Add(lastUpdateTimeParam);
            command.ExecuteNonQuery();
            connection.Dispose();
            connection.Close();
            Response.Redirect(Request.RawUrl);
        }

        private Boolean validate()
        {
            return titleTextBox.Text.ToString().Trim().Length > 0 && bodyTextBox.Text.ToString().Trim().Length > 0;
        }

        private void cleanInputs()
        {
            NewNoteTitleLabel.Text = "Yeni Not Ekle";
            editNote = null;
            titleTextBox.Text = "";
            bodyTextBox.Text = "";
            closeButton.Visible = false;
            saveButton.Text = "Kaydet";
        }
    }
}