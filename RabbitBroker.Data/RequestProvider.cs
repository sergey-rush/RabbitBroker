using System;
using System.Data;
using System.Threading.Tasks;
using RabbitBroker.Core;
using Npgsql;
using NpgsqlTypes;

namespace RabbitBroker.Data
{
    public class RequestProvider : RequestManager
    {
        public async Task ProcessFee(int userId, decimal fee)
        {
            using (var cn = new NpgsqlConnection(ConnectionString))
            {
                await cn.OpenAsync().ConfigureAwait(false);

                using (NpgsqlCommand cmd = new NpgsqlCommand("public.process_fee", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_user_id", NpgsqlDbType.Integer).Value = userId;
                    cmd.Parameters.Add("p_fee", NpgsqlDbType.Numeric).Value = fee;

                    await cmd.ExecuteNonQueryAsync();//.ConfigureAwait(false);
                }
            }
        }

        public void ProcessFee(object obj)
        {
            Message message = (Message)obj;
            try
            {
                using (NpgsqlConnection cn = new NpgsqlConnection(ConnectionString))
                {
                    cn.Open();
                    using (NpgsqlTransaction tran = cn.BeginTransaction())
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("public.process_fee", cn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("p_user_id", NpgsqlDbType.Integer).Value = message.UserId;
                            cmd.Parameters.Add("p_fee", NpgsqlDbType.Numeric).Value = message.Amount;
                            tran.Commit();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var df = ex.Message;
            }
        }

    }
}