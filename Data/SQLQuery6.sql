Alter table Keysgruppe add foreign key (Kom_nr) references Kommune;
Alter table Keysgruppe add foreign key (GruppeId) references Keysnumber;
Alter table Keysgruppe add foreign key (Aarstal) references Aarstal;



Create view GruppeRef As
Select Keysgruppe.Kom_nr, City, Gruppe, Aarstal, tal
From Keysgruppe Join Keysnumber on Keysgruppe.GruppeId = Keysnumber.Id
Join Kommune on Keysgruppe.Kom_nr = Kommune.Kom_nr