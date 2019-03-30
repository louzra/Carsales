using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Carsales.Models
{
    public class DataAccess
    {
        string connectionString = "Data Source = DESKTOP-5KVHUCQ\\SQLEXPRESS;  Initial Catalog = SQLExpress; integrated security=True";

        // Get VehicleType to be displayde in dropown
        public List<VehicleType> GetVehicleType()
        {
            List<VehicleType> vehicleTypes = new List<VehicleType>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.VehicleType", con);

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        VehicleType vehicleType = new VehicleType();

                        vehicleType.Type = rdr["Type"].ToString();

                        vehicleTypes.Add(vehicleType);
                    }
                }
            }

            return vehicleTypes;
        }
        
        // Soft delete the vehicle in the database
        // Set IsDeleted column to 1
        public void DeleteVehicle(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = string.Format("UPDATE VehicleProperty SET IsDeleted = 1 WHERE Id = {0}", id);
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }

        // Get all vehicles's common properties and display in Grid
        public IEnumerable<CommonProperty> GetAllVehicleDetails()
        {
            List<CommonProperty> commonProperties = new List<CommonProperty>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.VehicleProperty WHERE ISNULL(IsDeleted, 0) = 0", con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    CommonProperty commonProperty = new CommonProperty();

                    commonProperty.Id = Convert.ToInt32(rdr["Id"]);
                    commonProperty.VehicleType = rdr["VehicleType"].ToString();
                    commonProperty.Make = rdr["Make"].ToString();
                    commonProperty.Model = rdr["Model"].ToString();
                    commonProperty.Engine = rdr["Engine"].ToString();
                    commonProperty.BodyType = rdr["BodyType"].ToString();

                    commonProperties.Add(commonProperty);
                }
            }

            return commonProperties;
        }

        #region Common Properties
        public void InsertUpdateVehicle(VehicleProperty vehicleProperty)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("InsertUpdateVehicle", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", vehicleProperty.CommonProperty.Id);
                    cmd.Parameters.AddWithValue("@VehicleType", vehicleProperty.CommonProperty.VehicleType);
                    cmd.Parameters.AddWithValue("@Make", vehicleProperty.CommonProperty.Make);
                    cmd.Parameters.AddWithValue("@Model", vehicleProperty.CommonProperty.Model);
                    cmd.Parameters.AddWithValue("@Engine", vehicleProperty.CommonProperty.Engine);
                    cmd.Parameters.AddWithValue("@BodyType", vehicleProperty.CommonProperty.BodyType);

                    con.Open();

                    vehicleProperty.CommonProperty.Id = (int)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {

            }
        }

        // Get specifict vehicle's common property
        // To be displayed in Create/Details views
        public CommonProperty GetVehicleDetails(int Id)
        {
            CommonProperty commonProperty = new CommonProperty();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = string.Format("SELECT * FROM dbo.VehicleProperty WHERE Id = {0}", Id);
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    commonProperty.Id = Convert.ToInt32(rdr["Id"]);
                    commonProperty.VehicleType = rdr["VehicleType"].ToString();
                    commonProperty.Make = rdr["Make"].ToString();
                    commonProperty.Model = rdr["Model"].ToString();
                    commonProperty.Engine = rdr["Engine"].ToString();
                    commonProperty.BodyType = rdr["BodyType"].ToString();
                }
            }

            return commonProperty;
        }
        #endregion

        #region Car Properties
        // Execute stored procedure
        // To insert or update Car details in the database
        public void InsertUpdateCar(CarProperty CarProperty)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("InsertUpdateCar", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", CarProperty.Id);
                    cmd.Parameters.AddWithValue("@VehicleId", CarProperty.VehicleId);
                    cmd.Parameters.AddWithValue("@Doors", CarProperty.Doors);
                    cmd.Parameters.AddWithValue("@Wheels", CarProperty.Wheels);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }

        // Get Car details from the detabase
        public CarProperty GetCarDetails(int Id)
        {
            CarProperty carProperty = new CarProperty();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = string.Format("SELECT TOP 1 * FROM dbo.CarProperty WHERE VehicleId = {0} ORDER BY Id DESC", Id);
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        carProperty.Id = Convert.ToInt32(rdr["Id"]);
                        carProperty.Doors = Convert.ToInt32(rdr["Doors"]);
                        carProperty.Wheels = Convert.ToInt32(rdr["Wheels"]);
                    }
                }
            }

            return carProperty;
        }
        #endregion

        #region Boat Properties
        // Execute stored procedure
        // To insert or update Boat details in the database
        public void InsertUpdateBoat(BoatProperty BoatProperty)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("InsertUpdateBoat", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", BoatProperty.Id);
                    cmd.Parameters.AddWithValue("@VehicleId", BoatProperty.VehicleId);
                    cmd.Parameters.AddWithValue("@Length", BoatProperty.Length);
                    cmd.Parameters.AddWithValue("@PropulsionType", BoatProperty.PropulsionType);
                    cmd.Parameters.AddWithValue("@FishingRodHolders", BoatProperty.FishingRodHolders);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }

        // Get Boat details from the database
        public BoatProperty GetBoatDetails(int Id)
        {
            BoatProperty boatProperty = new BoatProperty();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = string.Format("SELECT TOP 1 * FROM dbo.BoatProperty WHERE VehicleId = {0} ORDER BY Id DESC", Id);
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        boatProperty.Id = Convert.ToInt32(rdr["Id"]);
                        boatProperty.Length = Convert.ToInt32(rdr["Length"]);
                        boatProperty.PropulsionType = rdr["PropulsionType"].ToString();
                        boatProperty.FishingRodHolders = rdr["FishingRodHolders"].ToString();
                    }
                }
            }

            return boatProperty;
        }
        #endregion
    }
}