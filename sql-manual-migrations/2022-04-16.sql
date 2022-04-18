/* AGENTS */
ALTER TABLE Agentes ADD Contact varchar(100) NULL 
ALTER TABLE Agentes ADD Email varchar(100) NULL 
ALTER TABLE Agentes ADD Phone varchar(100) NULL
UPDATE Agentes set Contact = '', Email = '', Phone = ''
ALTER TABLE Agentes ALTER COLUMN Contact varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
ALTER TABLE Agentes ALTER COLUMN Email varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
ALTER TABLE Agentes ALTER COLUMN Phone varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
/* AGENTS */

/* Shippers */
ALTER TABLE Shippers ADD Address varchar(100) NULL 
UPDATE Shippers set Address = '' 
ALTER TABLE Shippers ALTER COLUMN Address varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
/* Shippers */


/* Consignatarios */
ALTER TABLE Consignatarios ADD LegalId varchar(100) NULL 
UPDATE Consignatarios set LegalId = '' 
ALTER TABLE Consignatarios ALTER COLUMN LegalId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
/* Consignatarios */



/* SI */
ALTER TABLE ShippingInstructions ADD Insurance bit DEFAULT 0 NULL 
ALTER TABLE ShippingInstructions ADD CustomClearance bit DEFAULT 0 NULL 
ALTER TABLE ShippingInstructions ADD LocalDelivery bit DEFAULT 0 NULL 
UPDATE ShippingInstructions set Insurance = 0, CustomClearance = 0, LocalDelivery = 0
/* SI */





/* Report */

CREATE procedure [getShippingInstructionsChartReport](
    @desde datetime,
    @hasta datetime,
    @UsuarioID varchar(100)
)
as
SELECT 

	FORMAT(CAST(si.Fecha AS DATE),'yyyy-MM') as name,
	SUM(si.Total) as [value]
	
from 
    ShippingInstructions si
where 
    si.Fecha between @desde and DATEADD(day,1,@hasta) 
    group by FORMAT(CAST(si.Fecha AS DATE),'yyyy-MM') 
    ORDER BY FORMAT(CAST(si.Fecha AS DATE),'yyyy-MM') ASC

/* Report */