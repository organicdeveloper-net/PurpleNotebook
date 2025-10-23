the following will add a menu bar
<Menu>
    <MenuItem Header="File">
        <MenuItem Header="New Note" Click="NewNote_Click"/>
        <MenuItem Header="Save" Click="SaveNote_Click"/>
        <MenuItem Header="Save As" Click="SaveNoteAs_Click"/>
        <Separator/>
        <MenuItem Header="Exit" Click="ExitApp_Click"/>
    </MenuItem>
    <MenuItem Header="Help">
        <MenuItem Header="About" Click="ShowAbout_Click"/>
    </MenuItem>
</Menu>

