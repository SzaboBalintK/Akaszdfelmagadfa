   M S B A M L   `   `   `  ����D  @akasztofa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:akasztofa	akasztofa  5          akasztofa.Mainoldal   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:akasztofa     5        N� Title$  	Mainoldal��5	       N� 
Background
 � ���.��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      .+�5   
   �� -   5         ��Name$ program_label��5      $��1��6'   $��0��67   $��
Akasztófa��6D   $��Center=�6X   $	��Top=�6u   $��25(�6�   �� 5   
   -   5        $ jatekos_neve_label��5      $��1��6,   $��0��6<   $��Játékos neve:��6I   $��Center=�6a   $��Center=�6~   $��18(�6�   �� 5   
   -   5         ��Name$ jatekos_nev��5      $��1��6(   $��0��68   $��Center=�6F    �� TextWrapping$
 Wrap=�6c   $��Bottom=�6w   $	��200��6�   $��27��6�   �� 5   
   $��1��6   $��1��6    $��Témakör Választás:��6-   $��Center=�6J   $	��Top=�6g   $��20(�6   �� 5   
   -   5         ��Name$ biosz��5      $��0��6    $��1��60   $��	Biológia��6=   $
��Left=�6P   $��Bottom=�6k   $��10q�6�   6�   �� 5   
   -   5        $
 info��5      $��0��6   $��1��6/   $��Informatika��6<   $��Bottom=�6R   $��Right=�6�   $��10q�6�   �� 5   
   -   5        $ matek��5      $��1��6    $��1��60   $��
Matematika��6=   $��Center=�6R   $��Bottom=�6o   $��10q�6�   �� 5    
   -   5        $ mondatok_btn��5       $��2��6'   $��1��67   $��Közmondások��6D   $��Center=�6Z   $��Bottom=�6w   $��10q�6�   �� 5!   
   -   5        $ tovabb��5!      $��2��6!   $��2��61   $��Tovább��6>   $��Right=�6O   $��Bottom=�6k   $��20q�6�   $	��120��6�   �� 5"   
   -	   5        $ 
hibauzenet��5"      $&�� Ide lesz majd hibauzenet, ha van��6$   $
��Left=�6O   $	��270��6j   $	��Top=�6v   5$      5%      