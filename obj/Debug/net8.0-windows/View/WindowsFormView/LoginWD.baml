   M S B A M L   `   `   `  ����i  eSTUDENT-VERIFICATION-SYSTEM-THIRD-YEAR-PROJECT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Fclr-namespace:STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel8STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel  5   E   Q    KSTUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.WindowsFormView.LoginWD   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ^ ZMahApps.Metro.IconPacks, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am iMahApps.Metro.IconPacks.BootstrapIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.BoxIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj	 fMahApps.Metro.IconPacks.CircumIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag
 cMahApps.Metro.IconPacks.Codicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Coolicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Entypo, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.EvaIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak gMahApps.Metro.IconPacks.FeatherIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.FileIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Fontaudio, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.FontAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Fontisto, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.ForkAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.GameIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Ionicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.JamIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Lucide, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am iMahApps.Metro.IconPacks.MaterialDesign, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.MaterialLight, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.MemoryIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af bMahApps.Metro.IconPacks.Microns, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Modern, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Octicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al  hMahApps.Metro.IconPacks.PhosphorIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai! eMahApps.Metro.IconPacks.PicolIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al" hMahApps.Metro.IconPacks.PixelartIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai# eMahApps.Metro.IconPacks.RadixIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah$ dMahApps.Metro.IconPacks.RemixIcon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai% eMahApps.Metro.IconPacks.RPGAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj& fMahApps.Metro.IconPacks.SimpleIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag' cMahApps.Metro.IconPacks.Typicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af( bMahApps.Metro.IconPacks.Unicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj) fMahApps.Metro.IconPacks.VaadinIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak* gMahApps.Metro.IconPacks.WeatherIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah+ dMahApps.Metro.IconPacks.Zondicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a�Icon-http://metro.mahapps.com/winfx/xaml/iconpacks&    	 
                        ! " # $ % & ' ( ) * + 5   
   �	viewmodelFclr-namespace:STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel&    	 
                        ! " # $ % & ' ( ) * + 6E     � WindowStartupLocation$  CenterScreen=�5   	    � WindowStyle$
 none=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � AllowsTransparency$
 True��5   	   
������  � Title$ 
MainWindow��5   	   $	��500��5   	   $	��800��5   	    J� InputBindings 5      �� 5   
   
 �� Key$ Enter��6    �� Command 6!   �� *loginCommand+.��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��1.5* �6   .+�5   
   �� -   5        	 ��Name$	 CloseBtn��5      
 K� ZIndex$
 1��6:   $��20��6K   $��20��6V    � Cursor$
 Hand��6b   $��0q�6p   
������ 
���   �$��450,10,10,470q�6�   $��1��6�   .��5       ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   $��Center=�6O   .+�5!      .  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$ WindowClose=�6(   $��10��6<   
�������6I   5$      5'      �� 5,   
    �� CornerRadius$ 	10 0 0 10��6   ��5-      �� 5/       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 1,1&�67   .��50      �  � Color$ #3D7E35��6#    � Offset$ 0Q�63   52      ._�55      � .+�57      �� .+�58      �  � Points �    -1��� ;X�����#4eb186 J� Opacity$ .1Q�6J   � 59      ' �    �� �� ���� ��
��
�������$	 .05Q�6M   A� 5:      $��250 450 0 0q�6   $��40��63   $��40��6>   
�������$
 0.05Q�6W   A� 5;      $��
50 400 0 0q�6   $��20��62   $��20��6=   
�������$
 0.05Q�6V   5<      �� 5?      $��Center=�6   .+�5@      �� $��Welcome��6       	titleText#��[  6/   �� 5A      $7��1Log in to access the Student Verification System!��6      descriptionText#��[ 6Y   $ .8Q�6�   $��20 30q�6�   5B      5C      5D      �� 5G   
   -   5        ���#ffffff$��1��5G   &   $ 	0 10 10 0��66   ._�5H      �� $��Center=�6   .+�5J      �� $1��+pack://application:,,,/Resources/LogoUm.png��6   $	��100��5K      $��0,8,0,0q�5L      �� 5O      $ 
Horizontal=�6   $
��0 20q�66   $��Center=�6D   �� 5T      $��1q�6   ���#acb0af$
��70 7q�6C   $ 5��6Q   ._�5U      � $	��7 9q�6    5V      �� 5W      $
��auto �6/   �� 5X      $��* �6/   .+�5[        $ Email=�60   $��20��6=   $	 0.5Q�6I   
�������6W   �� 5\      -   5         ��Name$ 	textEmail��5\   $   $��username��6W     textHint#��[ 6g   �� 5]      -   5         ��Name$ txtEmail��5]   "    �� 	MaxLength$ 50��6q     textbox#��[ 6�   ��64   �� *
Username+6�   5^      5_      �� 5c      $��1q�6   ���#acb0af$
��70 7q�6C   $ 5��6Q   ._�5d      � $	��7 9q�6    5e      �� 5f      $
��auto �6/   �� 5g      $��* �6/   �� 5h      $��18 �6/   .+�5k        $
 Lock=�60   $��20��6<   $	 0.5Q�6H   
�������6V   �� 5l      -   5        $ textPassword��5l   $   $��password��6_   #��[ 6o   G� 5m      -   5         G�Name$ txtPassword��5m   &    G� 	MaxLength$ 30��6;   #��[ 6�   �� 5n      -   5        $ txtPasswordUnmask��5n   "   $ 30��6=   $y�	Collapsed=�6�   #��[ 6�   ��6M   �� *
Password+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5r      -   5        $	 EyeBtn��5r   !   $y�	Collapsed=�62   $��20��6^   $��20��6j   
������ $��0q�6�   $��2��6�   .��5t        -	   5          Name$ EyeIcon��5t   4   $��2��6E   $	 Eye=�6U   $��20��6`   $	 0.5Q�6l   
�������6z   5v      �� 5y      -
   5        $	 	OffEyeBtn��5y   !   $��20��6L   $y�	Collapsed=�6X   $��20��6o   
������ $��0q�6�   $��2��6�   .��5{        -   5        $ 
EyeOffIcon��5{   4   $��2��6H   $ EyeOff=�6X   $��20��6f   $	 0.5Q�6r   
�������6�   5}      5      5�      �� 5�      -   5        $ 
forgotpass��5�      $��forgot password?��6Q   #��[ 6i   $��	Underline{�6�   $��Italic$�6�   ���#878787$��	0 10 0 15q�6�   �� 5�      $��SignIn��6     signInButton#��[ 6L   ��6*   �� *loginCommand+6L   5�      5�      5�      5�      