require 'test_helper'

class SubjectsControllerTest < ActionController::TestCase
  include Devise::TestHelpers


  def setup
    @request.env["devise.mapping"] = Devise.mappings[:school_admin]
    sign_in school_admins(:one)
  end


  # setup do
  #   @subject = subjects(:one)
  # end

  # test "should get index" do
  #   get :index
  #   assert_response :success
  #   assert_not_nil assigns(:subjects)
  # end

  # test "should get new" do
  #   get :new
  #   assert_response :success
  # end

  test "should create subject" do
    assert_difference('Subject.count') do
      post :create, subject: { code: 'MyString3', name: 'koko' }
    end

    assert_redirected_to view_school_subjects_school_admins_path
  end

  test "should not create subject with token code" do
    assert_difference('Subject.count') do
      post :create, subject: { code: 'MyString3', name: 'koko' }
    end
    assert_no_difference('Subject.count') do
      post :create, subject: { code: 'MyString3', name: 'lolo' }
    end

    assert_redirected_to add_subject_school_admins_path
  end

   test "should show subject" do
      @subject = subjects(:one)
       get :show, id: @subject
        assert_response :success
   end

  # test "should get edit" do
  #   get :edit, id: @subject
  #   assert_response :success
  # end

  # test "should update subject" do
  #   patch :update, id: @subject, subject: { code: @subject.code, name: @subject.name }
  #   assert_redirected_to subject_path(assigns(:subject))
  # end

  # test "should destroy subject" do
  #   assert_difference('Subject.count', -1) do
  #     delete :destroy, id: @subject
  #   end

  #   assert_redirected_to subjects_path
  # end
end
