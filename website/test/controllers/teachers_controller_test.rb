require 'test_helper'


class TeachersControllerTest < ActionController::TestCase
  include Devise::TestHelpers


  def setup
    @request.env["devise.mapping"] = Devise.mappings[:teacher]
    sign_in teachers(:one)
  end


  test "should get view game records" do
    get( :view_game_records)
    assert_response :success
    assert_not_nil assigns(:current_teacher)
    assert_not_nil assigns(:students)
    assert_template :view_game_records
    assert_template layout: "layouts/application"
  end
  

end