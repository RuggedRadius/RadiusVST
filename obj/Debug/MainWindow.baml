   M S B A M L   `   `   `  ����D  @VST-Synth, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:VST_Synth	VST_Synth  5   	       VST_Synth.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:VST_Synth     5   	   -   5          �Name$  wndMain��5   	    � Title$ Radius��5   	   $	��447��6   $	��729��6%   ��5	      
� 5
   
   .��5      �  � Color$ Black��6    � Offset$ 1Q�6)   � 5      $ 	#FF191919��6   5      .��5      � .+�5   
   �� -   5         ��Name$ groupBox��5      $��
Oscillator��6%   $
��Left=�69   $��10,5,0,0q�6T   $	��Top=�6f   $	��225��6~   
������� � UseLayoutRounding . .��5      �� .+�5      �� -   5         ��Name$ cmbOsc1Type��5      $��10q�60   �� 5      $��Octave��6   
�������6)   �� 5      -   5         ��Name$ sliderOsc1Octave��5      $��10q�63    � Minimum$ 2Q�6?   	 � Maximum$	 20Q�6K   
 � Value$
 11Q�6X   5      5      �� 5   
   -   5         ��Name$ image��5      $
��Left=�6   $	��215��6:   $��
85,160,0,0q�6G   $	��Top=�6[   $	��550��6s   $4��.pack://siteoforigin:,,,/Resources/download.gif��6   �� 5   
   -   5        $ groupBox_Copy��5      $��
Oscillator��6*   $
��Left=�6>   $��	245,5,0,0q�6Y   $	��Top=�6l   $	��225��6�   
������� . .��5      �� .+�5      �� -   5        $ cmbOsc1Type1��5      $��10q�61   �� 5      $��Octave��6   
�������6)   �� 5      -   5        $ sliderOsc2Octave��5      $��10q�63   $ 2Q�6?   $	 20Q�6K   $
 11Q�6X   5      5       �� 5!   
   -	   5        $ groupBox_Copy1��5!      $��
Oscillator��6+   $
��Left=�6?   $��	480,5,0,0q�6Z   $	��Top=�6m   $	��225��6�   
������� . .��5"      �� .+�5#      �� -
   5        $ cmbOsc1Type2��5#      $��10q�61   �� 5$      $��Octave��6   
�������6)   �� 5%      -   5        $ sliderOsc3Octave1��5%      $��10q�64   $ 2Q�6@   $	 20Q�6L   $
 11Q�6Y   5&      5(      5*      5+      