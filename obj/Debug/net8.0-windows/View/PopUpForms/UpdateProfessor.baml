   M S B A M L   `   `   `  ����i  eSTUDENT-VERIFICATION-SYSTEM-THIRD-YEAR-PROJECT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Fclr-namespace:STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel8STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel  5   E   T    NSTUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms.UpdateProfessor   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ^ ZMahApps.Metro.IconPacks, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am iMahApps.Metro.IconPacks.BootstrapIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.BoxIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj	 fMahApps.Metro.IconPacks.CircumIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag
 cMahApps.Metro.IconPacks.Codicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Coolicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Entypo, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.EvaIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak gMahApps.Metro.IconPacks.FeatherIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.FileIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Fontaudio, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.FontAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Fontisto, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.ForkAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.GameIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Ionicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.JamIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Lucide, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am iMahApps.Metro.IconPacks.MaterialDesign, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.MaterialLight, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.MemoryIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af bMahApps.Metro.IconPacks.Microns, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Modern, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Octicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al  hMahApps.Metro.IconPacks.PhosphorIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai! eMahApps.Metro.IconPacks.PicolIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al" hMahApps.Metro.IconPacks.PixelartIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai# eMahApps.Metro.IconPacks.RadixIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah$ dMahApps.Metro.IconPacks.RemixIcon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai% eMahApps.Metro.IconPacks.RPGAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj& fMahApps.Metro.IconPacks.SimpleIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag' cMahApps.Metro.IconPacks.Typicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af( bMahApps.Metro.IconPacks.Unicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj) fMahApps.Metro.IconPacks.VaadinIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak* gMahApps.Metro.IconPacks.WeatherIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah+ dMahApps.Metro.IconPacks.Zondicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a�Icon-http://metro.mahapps.com/winfx/xaml/iconpacks&    	 
                        ! " # $ % & ' ( ) * + 5   
   �	viewmodelFclr-namespace:STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel&    	 
                        ! " # $ % & ' ( ) * + 6E     � Title$  UpdateProfessor��5   	   $	��450��5	   
   $	��800��6    � WindowStartupLocation$ CenterScreen=�5
   
    � WindowStyle$
 None=�5   
   
������  � AllowsTransparency$
 True��5   
    � 
ResizeMode$ NoResize=�5   
   .��5      ��  �� CornerRadius$ 10��6   ���#f7f5f5._�5   
   �  � RowDefinitions 5      �� 5      $��* �6     � ColumnDefinitions 5      �� 5      $	��600 �6#   �� 5      .+�5"      �� $��0��6   $ 20,20,20,20��6"   $��2��6=   $��0��6Q   
�������$��10,10,145,10q�6t      
formShadow J� Effect# [  6�   ._�5$      �  5%      �� 5&      $
��Auto �6(   �� 5'      $��* �6(   .+�5+      �� $��0��6   $��2��5,      ���#E4E0E1$��
10,42,10,0q�5.      $	��Top=�5/      �� 52      $ 20��6   
�������$��0,37,0,7q�53   %   $��2��67   ._�56      �  58      �� 5:   "   �� 5<   "   .+�5D      �� $
��Name��6$   $
��Bold"�63   $��137,14,134,344q�6F   �� 5G      -   5        	 ��Name$
	 Name��5G   &     TextBoxesForm#��[ 64   ���#bab7b6$��1q�5I      $��137,42,114,319q�63   
 �� 	MaxLength$
 50��5K      $��2��5M   M   ��5J      �� *Selected_professor.Name+ �� Mode$ TwoWay=�5M   M   �� 5Q      $��Gmail��6$   $
��Bold"�64   $��135,72,135,286q�6G   �� 5S      -   5        $	 Gmail��5S   &   #��[ 65   ���#bab7b6$��1q�5U      $��137,101,114,260q�62   $
 40��5W      $��2��5Y      ��5V      �� *Selected_professor.Email+$ TwoWay=�5Y      �� 5^      $	��Age��6$   $
��Bold"�62   $��137,191,113,167q�6E   �� 5`      -   5        $		 Age��5`   &   #��[ 63   ���#bab7b6$
 2��5b      $��1q�5d      $��137,220,114,141q�61   $��2��5e   R   ��6   �� *Selected_professor.Age+$ TwoWay=�6R   �� 5l      $��Address��6$   $
��Bold"�66   $��137,131,113,227q�6I   �� 5n      -   5        $	 Address��5n   &   #��[ 67   ���#bab7b6$	
 100��5p      $��1q�5r      $��137,161,114,200q�63   $��2��5s   X   ��6   �� *Selected_professor.Address+$ TwoWay=�6X   �� 5x      $��
Department��6$   $
��Bold"�69   $��137,245,102,113q�6K   �� 5z      $��137,278,115,0q�5{   "   $	��Top=�5|   "   ���#bab7b6$��1q�5~   "   $��26��66    �� DisplayMemberPath$
 Name��5�   "    �� SelectedValuePath$ DepartmentID��5�   "   $��2��5�   i   ��5   "   �� *DepartmentCollection+ �� SelectedValue 5�   "   �� *!Selected_professor.DepartmentID+$ TwoWay=�6i   �� 5�        SubmitButton#��[ 6%   $
��Save��5�      $��1��6-   $��40,333,0,23q�6=   $	��168��5�      $
��Left=�5�      $��30��5�      $
��Bold"�5�      $��0q�5�      ��5�      �� *UpdateProfessorrsCommand+5�      �� 5�        ClearButton#��[ 6%   $��Clear��5�      $��137,333,0,23q�6.   $	��168��5�      $
��Left=�5�      $��30��5�      $
��Bold"�5�      
��� � ���5�      �� *ClearCommand+5�      5�      5�      .  (MahApps.Metro.IconPacks.PackIconMaterial  5�         Kind$ BookEducation=�6,   ���#3D7E35  menuButtonIcon#��[ 6V   $��10,0,0,0q�6~   �� 5�      $��Professor Details��6    $
��Bold"�69   $��30,13,502,9q�6L   $��Helvetica Neue)�6a   5�      5�      �� 5�      $��0��6   $��1��5�       K� ZIndex$ -1��6   ���#3D7E35$��10,0,0,0q�5�        
dropShadow# [ 5�      $ 10��5�      ._�5�      � .+�5�      �� -   5         ��Name$ Close��5�        MinimizeButtonStyle#��[ 5�      $��30��5�      $��30��6%   $��150,10,10,410q�61   .��5�        -   5          Name$ CloseWindow��5�      $ WindowClose=�5�      $��10��5�       J� RenderTransformOrigin$ 0.803,0.533&�6*   $��12��5�        WindowControlIcon#��[ 5�       � Cursor$
 Hand��5�      $��Center=�6,   5�      �� 5�      $
��Wrap=�6    
�������$��64,52,10,331q�5�      $��Helvetica Neue)�5�      $��14(�5�      FDHi Admin! Automate the Professor data insertion process effortlessly5�      �� 5�      -   5        $ 
Automation��5�      $��57,150,0,275q�5�      $
 Hand��5�      ���#FFFF  
Automation#��[ 5�      $��1q�5�      $
��Bold"�5�      $
��Left=�5�      $	��123��61   .��5�      ��  �� Orientation$ 
Horizontal=�6%   .+�5�        $ AutoMode=�64   #��[ 6D   �� 5�      $��Bulk Insert��6(   $��Roboto)�6;   $��62��6O   $��Center=�6Z   5�      5�      5�      5�      5�      5�      5�      