﻿//*******************************************************************//
//       Active Query Builder Component Suite                        //
//                                                                   //
//       Copyright © 2006-2019 Active Database Software              //
//       ALL RIGHTS RESERVED                                         //
//                                                                   //
//       CONSULT THE LICENSE AGREEMENT FOR INFORMATION ON            //
//       RESTRICTIONS.                                               //
//*******************************************************************//

using System;
using System.Data.Odbc;
using System.Windows;
using System.Windows.Input;

namespace BasicDemo.ConnectionWindow
{

    /// <summary>
    /// Interaction logic for ODBCConnectionWindow.xaml
    /// </summary>
    public partial class ODBCConnectionWindow
    {
        public string ConnectionString = "";

        public ODBCConnectionWindow()
        {
            InitializeComponent();
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonConnect_OnClick(object sender, RoutedEventArgs e)
        {
            OdbcConnectionStringBuilder builder = new OdbcConnectionStringBuilder();

            try
            {
                builder.ConnectionString = textBoxConnectionString.Text;

                Mouse.OverrideCursor = Cursors.Wait;

                using (OdbcConnection connection = new OdbcConnection(builder.ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        ConnectionString = builder.ConnectionString;
						DialogResult = true;
						Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Failed to connect.");
                    }
                    finally
                    {
                        Mouse.OverrideCursor = null;
                    }
                }
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show(ae.Message, "Invalid OLE DB connection string.");
            }
        }
    }
}
