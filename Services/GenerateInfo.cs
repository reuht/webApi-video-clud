using System.Data;
using Models;
using System.Linq;
using ContextAplication;
// using Microsoft.AspNetCore.Mvc;
using backEnd.DTOs;
using SpreadsheetLight;
using Microsoft.EntityFrameworkCore;

namespace backEnd.Services;


public class GenerateInfo {

    private readonly AplicationContext _context; 

    public GenerateInfo(AplicationContext context){
        _context = context; 
    }

    public async Task<SLDocument> GetExcelInfo(){
        try {

            var informeUsers = await _context.Users.Select(p => new InformeDTO {

                UserId = p.UserId,
                Name = p.Name,
                Email = p.Email,
                Identification= p.Identification,
                Adress = p.Adress

            }).ToListAsync(); 

            SLDocument firtSheetUser = new SLDocument(); 
            DataTable FirstTableUser = new DataTable();

            //Titles columns 
            FirstTableUser.Columns.Add("UserId", typeof(string));
            FirstTableUser.Columns.Add("Name", typeof(string));
            FirstTableUser.Columns.Add("Email", typeof(string));
            FirstTableUser.Columns.Add("Identification", typeof(string));
            FirstTableUser.Columns.Add("Adress", typeof(string));

            foreach(InformeDTO user in informeUsers){
                FirstTableUser.Rows.Add(user.UserId.ToString(), user.Name, user.Email, user.Identification, user.Adress);
            }
            Guid nameComplement = new Guid(); 

            firtSheetUser.ImportDataTable(1, 1, FirstTableUser, true);
            firtSheetUser.SaveAs($"New-Report{nameComplement.ToString()}.xlsx");

            return firtSheetUser;

        }catch(Exception ex){
            return null;
        }
    }
}