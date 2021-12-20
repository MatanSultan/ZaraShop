﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZaraShopProject
{
    public partial class EditBrand : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            BindGridview();
        }
        protected void txtID_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString);
            if (con.State == ConnectionState.Closed) { con.Open(); }
            SqlCommand cmd = new SqlCommand("select Name from tblBrands where BrandID=@ID", con);
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtFilterGrid1Record.Text));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(ds, "dt");
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnUpdateBrand.Enabled = true;
                txtUpdateBrandName.Text = ds.Tables[0].Rows[0]["Name"].ToString();

            }
            else
            {
                btnUpdateBrand.Enabled = false;
                txtUpdateBrandName.Text = string.Empty;
            }
            con.Close();
        }
        protected void btnUpdateBrand_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString);
            if (con.State == ConnectionState.Closed) { con.Open(); }
            SqlCommand cmd = new SqlCommand("update tblBrands set Name=UPPER(@Name) where BrandID=@ID", con);
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(TextBrandID.Text));
            cmd.Parameters.AddWithValue("@Name", txtUpdateBrandName.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('Update successfully')</script>");
            BindGridview();
            btnUpdateBrand.Text = string.Empty;
            txtUpdateBrandName.Text = string.Empty;


        }

        private void BindGridview()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString);
            if (con.State == ConnectionState.Closed) { con.Open(); }
            SqlDataAdapter da = new SqlDataAdapter("select * from tblBrands", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
    }
}