INSERT INTO [VolunteerPlayerRSVP].[dbo].[TeamPersonnel]
           (
           [TeamId]
           ,[VolunteerId]
           ,[UserId])
     VALUES
           (
           1
           ,12
           ,5)
GO


delete from TeamPersonnel
where TeamPersonnelId=15