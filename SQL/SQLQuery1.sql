use Sample
select * from QR_Data

drop table QR_Data

create Table QR_Data(
    [Label_ID] NVARCHAR(50) PRIMARY KEY,
    [Serial_Number] NVARCHAR(50),
    [Item_Name] TEXT,
    [Item_ID] NVARCHAR(50));

	INSERT INTO QR_Data VALUES (N'XBCVKHJ8',N'0323456871024',N'Blender','I56')
	INSERT INTO QR_Data VALUES ('XhCVKyJ8','0323456871024','Blender','I56')
	INSERT INTO QR_Data (Label_ID,Serial_Number,Item_Name,Item_ID) VALUES ('0YfYVeGn', '0123ASD1011', '1:6 Shaquille ONeal', 'ASD')