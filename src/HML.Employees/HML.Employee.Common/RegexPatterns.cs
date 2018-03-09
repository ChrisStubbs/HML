namespace HML.Employee.Common
{
	public class RegexPatterns
	{
		/// <summary>
		/// Postal codes in the U.K.
		///	are composed of five to seven alphanumeric characters separated by a space. 
		/// The rules covering which characters can appear at particular positions are rather complicated and fraught with exceptions.
		/// The regular expression therefore sticks to the basic rules.
		/// </summary>
		public static string Postcode => @"^[A-Z]{1,2}[0-9R][0-9A-Z]? [0-9][ABD-HJLNP-UW-Z]{2}$";

		/// <summary>
		/// A National Insurance number (also called a NINO) consists of exactly nine characters, 
		///	made up of the following components: A two-letter prefix.Six digits.
		/// A one-character suffix, consisting of either a letter in the range A to D, or a space.
		///
		///	^                      # Beginning of string
		///[A-CEGHJ-PR-TW-Z]{ 1}   # Match first letter, cannot be D, F, I, Q, U or V
		///[A-CEGHJ-NPR-TW-Z]{1}   # Match second letter, cannot be D, F, I, O, Q, U or V
		///[0-9]{6}                # Six digits
		///[A-D]{1}                # Match last letter can only be A, B, C or D
		///	$                      # End of string
		/// </summary>
		public static string NationalInsuranceNumber => @"^[A-CEGHJ-PR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[A-DFM]{0,1}$";
	}
}
